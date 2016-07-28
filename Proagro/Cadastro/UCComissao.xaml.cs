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
    /// Interaction logic for UCComissao.xaml
    /// </summary>
    public partial class UCComissao : UserControl
    {
        private proagroEntities proEnt;

        public UCComissao()
        {
            proEnt = new proagroEntities();
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            tipo_produtoComboBox.ItemsSource = proEnt.tipo_produto.ToList();
            tipo_vendedorComboBox.ItemsSource = proEnt.tipo_vendedor.ToList();

            /*porcentagem_minimaColumn.Binding.StringFormat = "{0:0.0} %";
            porcentagem_maximaColumn.Binding.StringFormat = "{0:0.0} %";
            desconto_maximoColumn.Binding.StringFormat = "{0:0.0} %";
            prazo_maximoColumn.Binding.StringFormat = "{0:0} dias";*/

            // Do not load your data at design time.
            // if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            // {
            // 	//Load your data here and assign the result to the CollectionViewSource.
            // 	System.Windows.Data.CollectionViewSource myCollectionViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["Resource Key for CollectionViewSource"];
            // 	myCollectionViewSource.Source = your data
            // }

            System.Windows.Data.CollectionViewSource proagroEntitiesViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("proagroEntitiescomissaoViewSource")));
            proagroEntitiesViewSource.Source = proEnt.comissao.ToList();
            comissaoDataGrid.SelectedIndex = -1;
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = true;
            if (char.IsDigit(e.Text, e.Text.Length - 1))
            {
                var textBox = (TextBox)sender;
                var text = textBox.Text;
                text += e.Text[e.Text.Length - 1];
                text = text.Replace(".", "");
                text = text.Insert(text.Length - 1, ".");

                textBox.Text = text;
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = (TextBox)sender;
            if (textBox.Text.Length > 0)
            {
                textBox.TextChanged -= TextBox_TextChanged;

                if(textBox.Text[textBox.Text.Length - 1] == '.')
                {
                    textBox.Text = textBox.Text.Remove(textBox.Text.Length - 1);
                    textBox.Text = textBox.Text.Insert(textBox.Text.Length - 1, ".");
                }

                textBox.Text = Convert.ToDecimal(textBox.Text).ToString("0.0");

                textBox.SelectionStart = textBox.Text.Length;
                textBox.TextChanged += TextBox_TextChanged;
            }


            /*
            var textBox = (TextBox)sender;
            if (textBox.Text.Length > 0)
            {
                textBox.TextChanged -= TextBox_TextChanged;

                if (!textBox.Text.Contains(","))
                    textBox.Text = Convert.ToDecimal(textBox.Text).ToString("0.0").Replace(".", ",");

                textBox.Text = textBox.Text.Replace(",", "");
                while (textBox.Text[0] == '0' && textBox.Text.Length > 2) textBox.Text = textBox.Text.Remove(0, 1);

                while (textBox.Text.Length < 2) textBox.Text = "0" + textBox.Text;
                textBox.Text = textBox.Text.Insert(textBox.Text.Length - 1, ",");

                textBox.SelectionStart = textBox.Text.Length;
                textBox.TextChanged += TextBox_TextChanged;
            }
            */
        }

        private void prazo_maximoTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
                e.Handled = true;
        }

        private void btGravar_Click(object sender, RoutedEventArgs e)
        {
            if (comissaoDataGrid.SelectedIndex == -1)
            {
                proEnt.comissao.Add(new comissao()
                {
                    tipo_produto = (long)tipo_produtoComboBox.SelectedValue,
                    tipo_vendedor = (long)tipo_vendedorComboBox.SelectedValue,
                    desconto_maximo = Convert.ToDecimal(desconto_maximoTextBox.Text.Replace(',', '.')),
                    porcentagem_maxima = Convert.ToDecimal(porcentagem_maximaTextBox.Text.Replace(',', '.')),
                    porcentagem_minima = Convert.ToDecimal(porcentagem_minimaTextBox.Text.Replace(',', '.')),
                    prazo_maximo = Convert.ToInt32(prazo_maximoTextBox.Text)
                });
            }
            proEnt.SaveChanges();

            System.Windows.Data.CollectionViewSource proagroEntitiesViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("proagroEntitiescomissaoViewSource")));
            proagroEntitiesViewSource.Source = proEnt.comissao.ToList();
            comissaoDataGrid.SelectedIndex = -1;
        }

        private void btNovo_Click(object sender, RoutedEventArgs e)
        {
            comissaoDataGrid.SelectedIndex = -1;
        }
    }
}
