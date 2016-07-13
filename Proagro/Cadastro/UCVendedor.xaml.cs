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
    /// Interaction logic for UCVendedor.xaml
    /// </summary>
    public partial class UCVendedor : UserControl
    {
        private proagroEntities proEnt;

        public UCVendedor()
        {
            proEnt = new proagroEntities();
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            tipo_vendedorComboBox.ItemsSource = proEnt.tipo_vendedor.ToList();

            // Do not load your data at design time.
            // if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            // {
            // 	//Load your data here and assign the result to the CollectionViewSource.
            // 	System.Windows.Data.CollectionViewSource myCollectionViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["Resource Key for CollectionViewSource"];
            // 	myCollectionViewSource.Source = your data
            // }

            System.Windows.Data.CollectionViewSource proagroEntitiesViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("proagroEntitiesvendedorViewSource")));
            proagroEntitiesViewSource.Source = proEnt.vendedor.ToList();
            vendedorDataGrid.SelectedIndex = -1;
        }

        private void btSelecionarPessoa_Click(object sender, RoutedEventArgs e)
        {
            var consulta = new Consulta.Pessoa();
            consulta.ShowDialog();
            long id = consulta.retornaSelecao();
            if (id > 0)
            {
                var pessoa = proEnt.pessoa.Where(x => x.id == id).ToList()[0];
                pessoaTextBox.Text = pessoa.id.ToString();
                pessoaTextBlock.Text = pessoa.nome;
            }
        }

        private void btGravar_Click(object sender, RoutedEventArgs e)
        {
            if (vendedorDataGrid.SelectedIndex == -1)
            {
                proEnt.vendedor.Add(new vendedor()
                {
                    pessoa = Convert.ToInt64(pessoaTextBox.Text),
                    tipo_vendedor = (long)tipo_vendedorComboBox.SelectedValue,
                    usuario = usuarioTextBox.Text,
                    senha = senhaTextBox.Text
                });
            }
            proEnt.SaveChanges();

            System.Windows.Data.CollectionViewSource proagroEntitiesViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("proagroEntitiesvendedorViewSource")));
            proagroEntitiesViewSource.Source = proEnt.vendedor.ToList();
            vendedorDataGrid.SelectedIndex = -1;
        }

        private void btNovo_Click(object sender, RoutedEventArgs e)
        {
            vendedorDataGrid.SelectedIndex = -1;

        }

        private void ExibirUsuarioSenha(bool exibir)
        {
            usuarioLabel.Visibility = usuarioTextBox.Visibility = senhaLabel.Visibility = senhaTextBox.Visibility = 
                exibir ? Visibility.Visible : Visibility.Collapsed;
        }

        private void vendedorDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (vendedorDataGrid.SelectedIndex > -1)
                ExibirUsuarioSenha(false);
            else
            {
                Limpar();
                ExibirUsuarioSenha(true);
            }
        }

        private void Limpar()
        {
            pessoaTextBox.Text = "";
            pessoaTextBlock.Text = "";
            tipo_vendedorComboBox.SelectedValue = null;
            usuarioTextBox.Text = "";
            senhaTextBox.Text = "";
        }
    }
}
