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
    /// Interaction logic for Pessoa.xaml
    /// </summary>
    public partial class Pessoa : Window
    {
        private bool selecionou = false;

        public Pessoa()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource proagroEntitiesViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("proagroEntitiespessoaViewSource")));
            // Load data by setting the CollectionViewSource.Source property:

            var proEnt = new proagroEntities();
            proagroEntitiesViewSource.Source = proEnt.pessoa.Where(x => x.id > -1).ToList();
        }

        public long retornaSelecao()
        {
            return selecionou ? (pessoaDataGrid.SelectedItem as pessoa).id : -1;
        }

        private void btSelecionar_Click(object sender, RoutedEventArgs e)
        {
            selecionou = true;
            Close();
        }
    }
}
