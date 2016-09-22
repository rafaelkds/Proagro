using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for UCPedido.xaml
    /// </summary>
    public partial class UCPedido : UserControl
    {
        private proagroEntities proEnt;
        private pedido pedidoEdicao;

        public UCPedido()
        {
            proEnt = new proagroEntities();
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var lista = proEnt.vendedor.ToList();
            var listaAux = new List<ItemVendedor>(lista.Count);
            foreach (var item in lista)
                listaAux.Add(new ItemVendedor() {
                    Id = item.pessoa,
                    Desc = item.pessoa1.nome
                });
            vendedorComboBox.ItemsSource = vendedor_comissaoComboBox.ItemsSource = listaAux;

            //vendedorComboBox.ItemsSource = vendedor_comissaoComboBox.ItemsSource = proEnt.vendedor.ToList();
            //System.Windows.Data.CollectionViewSource proagroEntitiesViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("proagroEntitiesvendedorViewSource")));
            //proagroEntitiesViewSource.Source = proEnt.vendedor.ToList();

            


            NovoPedido();
        }

        private void btAdicionarProduto_Click(object sender, RoutedEventArgs e)
        {
            pedidoEdicao.pedido_produto.Add(new pedido_produto() { data_entrega = DateTime.Today });
            pedido_produtoDataGrid.Items.Refresh();
            pedido_produtoDataGrid.SelectedIndex = pedido_produtoDataGrid.Items.Count - 1;
        }

        private void btRemoverProduto_Click(object sender, RoutedEventArgs e)
        {
            if(pedido_produtoDataGrid.SelectedIndex > -1)
            {
                
                proEnt.pedido_produto.Remove((pedido_produto)pedido_produtoDataGrid.SelectedItem);
                //pedidoEdicao.pedido_produto.Remove(pedidoEdicao.pedido_produto.ElementAt(pedido_produtoDataGrid.SelectedIndex));
                pedido_produtoDataGrid.Items.Refresh();
                /*
                System.Windows.Data.CollectionViewSource proagroEntitiesViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("proagroEntitiespedidopedido_produtoViewSource")));
                var lista = proagroEntitiesViewSource.Source;
                proagroEntitiesViewSource.Source = null;
                proagroEntitiesViewSource.Source = lista;
                */
                new Task(AtualizarValores).Start();
            }
        }

        private void btSelecionarProduto_Click(object sender, RoutedEventArgs e)
        {
            var consulta = new Consulta.Produto();
            consulta.ShowDialog();
            long id = consulta.retornaSelecao();
            if (id > 0)
            {
                var produto = proEnt.produto.Where(x => x.id == id).ToList()[0];
                pedidoEdicao.pedido_produto.ElementAt(pedido_produtoDataGrid.SelectedIndex).produto = produto.id;
                produtoTextBlock.Text = produto.descricao;
            }
        }

        private void btAdicionarReceber_Click(object sender, RoutedEventArgs e)
        {
            pedidoEdicao.receber.Add(new receber() { data = DateTime.Today });
            receberDataGrid.Items.Refresh();
            receberDataGrid.SelectedIndex = receberDataGrid.Items.Count - 1;
        }

        private void btRemoverReceber_Click(object sender, RoutedEventArgs e)
        {
            if (receberDataGrid.SelectedIndex > -1)
            {
                proEnt.receber.Remove((receber)receberDataGrid.SelectedItem);
                //pedidoEdicao.receber.Remove(pedidoEdicao.receber.ElementAt(receberDataGrid.SelectedIndex));
                receberDataGrid.Items.Refresh();
                /*
                System.Windows.Data.CollectionViewSource proagroEntitiesViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("proagroEntitiespedidoreceberViewSource")));
                var lista = proagroEntitiesViewSource.Source;
                proagroEntitiesViewSource.Source = null;
                proagroEntitiesViewSource.Source = lista;
                */
                new Task(AtualizarValores).Start();
            }
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = true;
            if (char.IsDigit(e.Text, e.Text.Length - 1))
            {
                var textBox = (TextBox)sender;
                var text = textBox.Text;
                text += e.Text[e.Text.Length - 1];
                //text = text.Replace(".", "");
                //text = text.Insert(text.Length - (textBox.Name.StartsWith("quantidade") ? 3 : 2), ".");

                textBox.Text = text;
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = (TextBox)sender;
            if (textBox.Text.Length > 0)
            {
                textBox.TextChanged -= TextBox_TextChanged;

                if (textBox.Text.Contains("."))
                {
                    /*if (textBox.Text[textBox.Text.Length - 1] == '.')
                    {
                        textBox.Text = textBox.Text.Remove(textBox.Text.Length - 1);
                        textBox.Text = textBox.Text.Insert(textBox.Text.Length - (textBox.Name.StartsWith("quantidade") ? 3 : 2), ".");
                    }*/

                    int i = textBox.Text.Split('.')[1].Length;
                    textBox.Text = textBox.Text.Replace(".", "");
                    if (i != (textBox.Name.StartsWith("quantidade") ? 3 : 2))
                        textBox.Text = textBox.Text.Insert(textBox.Text.Length - ((textBox.Name.StartsWith("quantidade") ? 3 : 2)), ".");

                }

                textBox.Text = Convert.ToDecimal(textBox.Text).ToString(textBox.Name.StartsWith("quantidade") ? "0.000" : "0.00");
                
                textBox.SelectionStart = textBox.Text.Length;
                textBox.TextChanged += TextBox_TextChanged;
            }

            
        }

        private void btGravar_Click(object sender, RoutedEventArgs e)
        {
            /*if (((System.Windows.Data.CollectionViewSource)(this.FindResource("proagroEntitiespedidoViewSource"))).Source == null)
            {
                var novoPedido = new pedido()
                {
                    vendedor = (long)vendedorComboBox.SelectedValue,
                    vendedor_comissao = (long)vendedor_comissaoComboBox.SelectedValue,
                    numero = numeroTextBox.Text,
                    data = (DateTime)dataDatePicker.SelectedDate,
                    observacao = observacaoTextBox.Text
                };
                novoProduto.preco.Add(new preco()
                {
                    data = new DateTime(1980, 1, 1),
                    valor = Convert.ToDecimal(precoTextBox.Text.Replace(",", "."))
                });
                proEnt.produto.Add(novoProduto);
            }*/
            if (((System.Windows.Data.CollectionViewSource)(this.FindResource("proagroEntitiespedidoViewSource"))).Source == null)
                proEnt.pedido.Add(pedidoEdicao);
            
            proEnt.SaveChanges();
        }

        private void btSelecionarCliente_Click(object sender, RoutedEventArgs e)
        {
            var consulta = new Consulta.Pessoa();
            consulta.ShowDialog();
            long id = consulta.retornaSelecao();
            if (id > 0)
            {
                var pessoa = proEnt.pessoa.Where(x => x.id == id).ToList()[0];
                
                pedidoEdicao.pessoa1 = pessoa;
                //pedidoEdicao.pessoa = pessoa.id;
                //clienteTextBlock.Text = pessoa.nome;

                System.Windows.Data.CollectionViewSource proagroEntitiesViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("proagroEntitiespedidoViewSource")));
                var lista = new List<pedido>(1);
                lista.Add(pedidoEdicao);
                proagroEntitiesViewSource.Source = lista;
            }
        }

        private void NovoPedido()
        {
            System.Windows.Data.CollectionViewSource proagroEntitiesViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("proagroEntitiespedidoViewSource")));
            var listPedido = new List<pedido>(1);
            pedidoEdicao = new pedido()
            {
                data = DateTime.Today
                //pedido_produto = new List<pedido_produto>()
            };
            listPedido.Add(pedidoEdicao);
            proagroEntitiesViewSource.Source = listPedido;
            
            foreach (UIElement element in grid2.Children)
                element.IsEnabled = false;

            foreach (UIElement element in grid3.Children)
                element.IsEnabled = false;
        }

        private void receberDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (UIElement element in grid3.Children)
                element.IsEnabled = receberDataGrid.SelectedIndex > -1;
        }

        private void pedido_produtoDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (UIElement element in grid2.Children)
                element.IsEnabled = pedido_produtoDataGrid.SelectedIndex > -1;
        }

        private void AtualizarValores()
        {
            Thread.Sleep(200);
            this.Dispatcher.Invoke((Action)(() =>
            {
                decimal total = 0;
                foreach (pedido_produto item in pedido_produtoDataGrid.Items)
                    total += item.quantidade * item.valor;
            
                valor_totalTextBox.Text = total.ToString("0.00");

                total = 0;
                foreach (receber item in receberDataGrid.Items)
                    total += item.valor;

                valor_prazoTextBox.Text = total.ToString("0.00");
            }));
        }

        private void btCarregar_Click(object sender, RoutedEventArgs e)
        {
            var consulta = new Consulta.Pedido();
            consulta.ShowDialog();
            long id = consulta.retornaSelecao();
            if (id > 0)
            {
                System.Windows.Data.CollectionViewSource proagroEntitiesViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("proagroEntitiespedidoViewSource")));
                pedidoEdicao = proEnt.pedido.Where(x => x.id == id).First();
                var listPedido = new List<pedido>(1);
                listPedido.Add(pedidoEdicao);
                proagroEntitiesViewSource.Source = listPedido;

                new Task(AtualizarValores).Start();
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            new Task(AtualizarValores).Start();
        }

        private void btNovo_Click(object sender, RoutedEventArgs e)
        {
            NovoPedido();
        }

        private class ItemVendedor
        {
            public long Id { get; set; }
            public string Desc { get; set; }
        }
    }

        public class TotalConverter : IMultiValueConverter
        {
            public object Convert(object[] values, Type targetType,
                   object parameter, System.Globalization.CultureInfo culture)
            {
                decimal result = ((decimal)values[0]) * ((decimal)values[1]);
                return result.ToString("0.00");
            }
            public object[] ConvertBack(object value, Type[] targetTypes,
                   object parameter, System.Globalization.CultureInfo culture)
            {
                throw new NotSupportedException("Cannot convert back");
            }
        }
}
