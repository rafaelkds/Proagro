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
using System.Windows.Shapes;

namespace Proagro
{
    /// <summary>
    /// Interaction logic for Janela.xaml
    /// </summary>
    public partial class Janela : Window
    {
        public Janela()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void miCadastroPessoa_Click(object sender, RoutedEventArgs e)
        {
            GridPrincipal.Children.Clear();
            GridPrincipal.Children.Add(new Cadastro.UCPessoa());
        }

        private void miCadastroPedido_Click(object sender, RoutedEventArgs e)
        {
            GridPrincipal.Children.Clear();
            GridPrincipal.Children.Add(new Cadastro.UCPedido());
        }

        private void miCadastroTipoProduto_Click(object sender, RoutedEventArgs e)
        {
            GridPrincipal.Children.Clear();
            GridPrincipal.Children.Add(new Cadastro.UCTipoProduto());
        }

        private void miCadastroTipoVendedor_Click(object sender, RoutedEventArgs e)
        {
            GridPrincipal.Children.Clear();
            GridPrincipal.Children.Add(new Cadastro.UCTipoVendedor());
        }

        private void miCadastroProduto_Click(object sender, RoutedEventArgs e)
        {
            GridPrincipal.Children.Clear();
            GridPrincipal.Children.Add(new Cadastro.UCProduto());
        }

        private void miCadastroVendedor_Click(object sender, RoutedEventArgs e)
        {
            GridPrincipal.Children.Clear();
            GridPrincipal.Children.Add(new Cadastro.UCVendedor());
        }

        private void miCadastroComissao_Click(object sender, RoutedEventArgs e)
        {
            GridPrincipal.Children.Clear();
            GridPrincipal.Children.Add(new Cadastro.UCComissao());
        }
    }
}
