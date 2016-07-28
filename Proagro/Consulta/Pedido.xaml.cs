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

namespace Proagro.Consulta
{
    /// <summary>
    /// Interaction logic for Pedido.xaml
    /// </summary>
    public partial class Pedido : Window
    {
        private bool selecionou;

        public Pedido()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource proagroEntitiesViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("proagroEntitiespedidoViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            var proEnt = new proagroEntities();
            proagroEntitiesViewSource.Source = proEnt.pedido.ToList();
        }

        public long retornaSelecao()
        {
            return selecionou ? (pedidoDataGrid.SelectedItem as pedido).id : -1;
        }

        private void btSelecionar_Click(object sender, RoutedEventArgs e)
        {
            selecionou = true;
            Close();
        }
    }
}
