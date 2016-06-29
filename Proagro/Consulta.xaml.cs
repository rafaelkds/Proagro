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
    /// Interaction logic for Consulta.xaml
    /// </summary>
    public partial class Consulta : Window
    {
        private Npgsql.NpgsqlConnection con;

        public Consulta()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
/*
            var x = new proagroEntities();
            //var t = from c in x.cidade select c;
            //var a = x.cidade.FirstOrDefault();
            //var b = x.cidade.ToList();
            //MessageBox.Show(string.Format("{0}  {1}", a.nome, a.estado));
            System.Windows.Data.CollectionViewSource proagroEntitiesViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("proagroEntitiescidadeViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // proagroEntitiesViewSource.Source = [generic data source]
            //proagroEntitiesViewSource.Source = b;
            proagroEntitiesViewSource.Source = x.cidade.Where(z => z.id > -1).ToList();
*/



            //System.Windows.Data.CollectionViewSource teste = ((System.Windows.Data.CollectionViewSource)(this.FindResource("proagroEntitiesViewSource")));
            //teste.Source = x.cidade.




            /*
            con = new Npgsql.NpgsqlConnection(Proagro.Properties.Settings.Default.proagroConnectionString);
            con.Open();
            con.BeginTransaction();
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("DECLARE cursorcidade SCROLL CURSOR FOR SELECT * FROM cidade;", con);
            command.ExecuteNonQuery();
            */

            /*
            con = new Npgsql.NpgsqlConnection(Proagro.Properties.Settings.Default.proagroConnectionString);
            con.Open();
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("FETCH FORWARD 5 FROM cursorcidade;", con);
            var dbr = command.ExecuteReader();
            cidadeDataGrid.DataContext = dbr;*/
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {/*
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("FETCH FORWARD 5 FROM cursorcidade;", con);
            var dbr = command.ExecuteReader();
            cidadeDataGrid.DataContext = dbr;
            dbr.
            */

        }
    }
}
