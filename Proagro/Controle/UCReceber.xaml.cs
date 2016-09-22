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
using System.Windows.Threading;

namespace Proagro.Controle
{
    /// <summary>
    /// Interaction logic for UCReceber.xaml
    /// </summary>
    public partial class UCReceber : UserControl
    {
        private proagroEntities proEnt;
        private long idClienteFiltro;
        private List<receber> lista;

        public UCReceber()
        {
            proEnt = new proagroEntities();
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var lista = new List<ItemSituacao>(6);
            lista.Add(new ItemSituacao() { Descricao = "Não pagas", Situacao = ItemSituacao.Tipo.NaoPagas });
            lista.Add(new ItemSituacao() { Descricao = "Vencidas", Situacao = ItemSituacao.Tipo.Vencidas });
            lista.Add(new ItemSituacao() { Descricao = "À vencer", Situacao = ItemSituacao.Tipo.AVencer });
            lista.Add(new ItemSituacao() { Descricao = "Pagas não confirmadas", Situacao = ItemSituacao.Tipo.PagasNaoConfirmadas });
            lista.Add(new ItemSituacao() { Descricao = "Pagas confirmadas", Situacao = ItemSituacao.Tipo.PagasConfirmadas });
            lista.Add(new ItemSituacao() { Descricao = "Todas", Situacao = ItemSituacao.Tipo.Todas });
            filtroSituacaoComboBox.ItemsSource = lista;

            var listaVend = proEnt.vendedor.ToList();
            var listaAux = new List<ItemVendedor>(listaVend.Count);
            foreach (var item in listaVend)
                listaAux.Add(new ItemVendedor()
                {
                    Id = item.pessoa,
                    Desc = item.pessoa1.nome
                });
            filtroVendedorComboBox.ItemsSource = listaAux;

            // Do not load your data at design time.
            // if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            // {
            // 	//Load your data here and assign the result to the CollectionViewSource.
            // 	System.Windows.Data.CollectionViewSource myCollectionViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["Resource Key for CollectionViewSource"];
            // 	myCollectionViewSource.Source = your data
            // }

        }

        private class ItemSituacao
        {
            public enum Tipo
            {
                NaoPagas,
                Vencidas,
                AVencer,
                PagasNaoConfirmadas,
                PagasConfirmadas,
                Todas
            };
            public Tipo Situacao { get; set; }
            public string Descricao { get; set; }
        }

        private class ItemVendedor
        {
            public long Id { get; set; }
            public string Desc { get; set; }
        }

        private void btSelecionarCliente_Click(object sender, RoutedEventArgs e)
        {
            var consulta = new Consulta.Pessoa();
            consulta.ShowDialog();
            long id = consulta.retornaSelecao();
            if (id > 0)
            {
                var pessoa = proEnt.pessoa.First(x => x.id == id);
                clienteTextBlock.Text = pessoa.nome;
                idClienteFiltro = pessoa.id;
                btRemoverCliente.Visibility = Visibility.Visible;
            }
        }

        private void btRemoverCliente_Click(object sender, RoutedEventArgs e)
        {
            clienteTextBlock.Text = "";
            idClienteFiltro = 0;
            btRemoverCliente.Visibility = Visibility.Collapsed;
        }

        private void btBuscar_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Data.CollectionViewSource myCollectionViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["proagroEntitiesreceberViewSource"];

            var emissao = filtroDataEmissaoDatePicker.SelectedDate != null;
            var emissaoMenor = (bool)filtroAteDataRadioButton.IsChecked;
            var emissaoIgual = (bool)filtroEstaDataRadioButton.IsChecked;
            var emissaoDate = filtroDataEmissaoDatePicker.SelectedDate ?? DateTime.Now;

            var vencimento = filtroDataVencimentoDatePicker.SelectedDate != null;
            var vencimentoMenor = (bool)filtroAteVencimentoRadioButton.IsChecked;
            var vencimentoIgual = (bool)filtroEsteVencimentoRadioButton.IsChecked;
            var vencimentoDate = filtroDataVencimentoDatePicker.SelectedDate ?? DateTime.Now;

            var vendedor = Convert.ToInt64(filtroVendedorComboBox.SelectedValue ?? 0);

            

            myCollectionViewSource.Source = lista = (from x in proEnt.receber
            where x.pedido1.numero.StartsWith(filtroPedidoTextBox.Text)
                && idClienteFiltro > 0 ? x.pedido1.pessoa == idClienteFiltro : true
                && !emissao ? true : (
                    emissaoMenor ? DateTime.Compare(x.pedido1.data, emissaoDate) <= 0 :
                    emissaoIgual ? DateTime.Compare(x.pedido1.data, emissaoDate) == 0
                    : DateTime.Compare(x.pedido1.data, emissaoDate) >= 0)
                && !vencimento ? true : (
                    vencimentoMenor ? DateTime.Compare(x.data, vencimentoDate) <= 0 :
                    vencimentoIgual ? DateTime.Compare(x.data, vencimentoDate) == 0
                    : DateTime.Compare(x.data, vencimentoDate) >= 0)
                && vendedor <= 0 ? true :
                    (x.pedido1.vendedor == vendedor
                    || x.pedido1.vendedor_comissao == vendedor)
            select x).ToList();
        }

        private void btAdicionarBaixa_Click(object sender, RoutedEventArgs e)
        {
            receber receber = (receber)receberDataGrid.SelectedItem;
            receber.receber_movimentos.Add(new receber_movimentos()
            {
                data = (DateTime)dataBaixaDatePicker.SelectedDate,
                valor = Convert.ToDecimal(valorBaixaTextBox.Text),
                observacoes = observacoesTextBox.Text
            });
            proEnt.SaveChanges();
            receber_movimentosDataGrid.Items.Refresh();
        }

        private void btCancelarBaixa_Click(object sender, RoutedEventArgs e)
        {
            if (receber_movimentosDataGrid.SelectedItem == null)
                return;

            var selecionado = (receber_movimentos)receber_movimentosDataGrid.SelectedItem;
            if (selecionado.cancelado != null)
                return;
                            
            var resultado = MessageBox.Show("Cancelar baixa?", "Cancelar", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (resultado == MessageBoxResult.Yes)
            {
                selecionado.cancelado = DateTime.Now;
                proEnt.SaveChanges();
                receber_movimentosDataGrid.Items.Refresh();
            }
            
        }

        private void receberDataGrid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if(receberDataGrid.CurrentColumn == receberDataGrid.Columns[5])
            {
                var linha = ((receber)receberDataGrid.SelectedItem);
                linha.protesto = linha.protesto != null ? null : DateTime.Now as DateTime?;
                proEnt.SaveChanges();
                receberDataGrid.Items.Refresh();
            }
            else if (receberDataGrid.CurrentColumn == receberDataGrid.Columns[6])
            {
                var linha = ((receber)receberDataGrid.SelectedItem);
                linha.serasa = linha.serasa != null ? null : DateTime.Now as DateTime?;
                proEnt.SaveChanges();
                receberDataGrid.Items.Refresh();
            }
            else if (receberDataGrid.CurrentColumn == receberDataGrid.Columns[7])
            {
                var linha = ((receber)receberDataGrid.SelectedItem);
                linha.pago = linha.pago != null ? null : DateTime.Now as DateTime?;
                proEnt.SaveChanges();
                receberDataGrid.Items.Refresh();
            }
        }

        private void btCancelar_Click(object sender, RoutedEventArgs e)
        {
            if (receberDataGrid.SelectedItem == null)
                return;

            var selecionado = (receber)receberDataGrid.SelectedItem;
            if (selecionado.cancelado != null)
                return;

            var resultado = MessageBox.Show("Cancelar parcela?", "Cancelar", MessageBoxButton.YesNo, MessageBoxImage.Question);
            
            if (resultado == MessageBoxResult.Yes)
            {
                selecionado.cancelado = DateTime.Now;
                proEnt.SaveChanges();
                receberDataGrid.Items.Refresh();
            }
        }
    }
}
