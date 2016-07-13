using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Proagro.Cadastro
{
    /// <summary>
    /// Interaction logic for UCProduto.xaml
    /// </summary>
    public partial class UCProduto : UserControl
    {
        private proagroEntities proEnt;
        private List<produto> listProduto;

        public UCProduto()
        {
            proEnt = new proagroEntities();
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            tipo_produtoComboBox.ItemsSource = proEnt.tipo_produto.ToList();

            btNovo_Click(null, null);
            // Do not load your data at design time.
            // if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            // {
            // 	//Load your data here and assign the result to the CollectionViewSource.
            // 	System.Windows.Data.CollectionViewSource myCollectionViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["Resource Key for CollectionViewSource"];
            // 	myCollectionViewSource.Source = your data
            // }
        }

        private void btCarregar_Click(object sender, RoutedEventArgs e)
        {
            var consulta = new Consulta.Produto();
            consulta.ShowDialog();
            long id = consulta.retornaSelecao();
            if (id > 0)
            {
                System.Windows.Data.CollectionViewSource proagroEntitiesViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("proagroEntitiesprodutoViewSource")));
                listProduto = proEnt.produto.Where(x => x.id == id).ToList();
                proagroEntitiesViewSource.Source = listProduto;

                precoLabel.Visibility = Visibility.Collapsed;
                precoTextBox.Visibility = Visibility.Collapsed;
                spPrecos.Visibility = Visibility.Visible;
            }
        }

        private void btGravar_Click(object sender, RoutedEventArgs e)
        {
            if (((System.Windows.Data.CollectionViewSource)(this.FindResource("proagroEntitiesprodutoViewSource"))).Source == null)
            {
                var novoProduto = new produto()
                {
                    descricao = descricaoTextBox.Text,
                    tipo_produto = (long)tipo_produtoComboBox.SelectedValue
                };
                novoProduto.preco.Add(new preco()
                {
                    data = new DateTime(1980, 1, 1),
                    valor = Convert.ToDecimal(precoTextBox.Text.Replace(",", "."))
                });
                proEnt.produto.Add(novoProduto);
            }
            proEnt.SaveChanges();

            btNovo_Click(null, null);
        }

        private void btNovo_Click(object sender, RoutedEventArgs e)
        {
            ((System.Windows.Data.CollectionViewSource)(this.FindResource("proagroEntitiesprodutoViewSource"))).Source = null;
            precoLabel.Visibility = Visibility.Visible;
            precoTextBox.Visibility = Visibility.Visible;
            spPrecos.Visibility = Visibility.Collapsed;
            descricaoTextBox.Text = string.Empty;
            tipo_produtoComboBox.SelectedValue = null;
            precoTextBox.Text = "0";
        }

        private void btAdicionarPreco_Click(object sender, RoutedEventArgs e)
        {
            if (dataDatePicker.SelectedDate != null && valorTextBox.Text != "0,00")
            {
                var data = new preco
                {
                    data = (DateTime)dataDatePicker.SelectedDate,
                    valor = Convert.ToDecimal(valorTextBox.Text.Replace(",", "."))
                };
                listProduto[0].preco.Add(data);
                precoDataGrid.Items.Refresh();
            }
        }

        private void valorTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = true;
            var textBox = (TextBox)sender;
            if (char.IsDigit(e.Text, e.Text.Length - 1))
                textBox.Text += e.Text[e.Text.Length - 1];
        }

        private void valorTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = (TextBox)sender;
            textBox.TextChanged -= valorTextBox_TextChanged;

            textBox.Text = textBox.Text.Replace(",", "");
            while (textBox.Text[0] == '0' && textBox.Text.Length > 3) textBox.Text = textBox.Text.Remove(0, 1);

            while (textBox.Text.Length < 3) textBox.Text = "0" + textBox.Text;
            textBox.Text = textBox.Text.Insert(textBox.Text.Length - 2, ",");
            
            textBox.SelectionStart = textBox.Text.Length;

            textBox.TextChanged += valorTextBox_TextChanged;
        }
    }
}
