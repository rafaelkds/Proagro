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

namespace Proagro
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private Proagro.proagroDataSetTableAdapters.cidadeTableAdapter proagroDataSetcidadeTableAdapter;
        private Proagro.proagroDataSet proagroDataSet;
        System.Windows.Data.CollectionViewSource cidadeViewSource;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {/*
            var c = new Consulta();
            c.Show();
            */

            //GridPrincipal.Children.Add(new UCPessoa());




            /*Proagro.proagroDataSet*/ proagroDataSet = ((Proagro.proagroDataSet)(this.FindResource("proagroDataSet")));
            // Load data into the table cidade. You can modify this code as needed.
            /*Proagro.proagroDataSetTableAdapters.cidadeTableAdapter*/ proagroDataSetcidadeTableAdapter = new Proagro.proagroDataSetTableAdapters.cidadeTableAdapter();
            proagroDataSetcidadeTableAdapter.Fill(proagroDataSet.cidade);
            /*System.Windows.Data.CollectionViewSource*/ cidadeViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("cidadeViewSource")));
            cidadeViewSource.View.MoveCurrentToFirst();

            // Load data into the table pessoa. You can modify this code as needed.
            Proagro.proagroDataSetTableAdapters.pessoaTableAdapter proagroDataSetpessoaTableAdapter = new Proagro.proagroDataSetTableAdapters.pessoaTableAdapter();
            proagroDataSetpessoaTableAdapter.Fill(proagroDataSet.pessoa);
            System.Windows.Data.CollectionViewSource pessoaViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("pessoaViewSource")));
            pessoaViewSource.View.MoveCurrentToFirst();
            
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            proagroDataSetcidadeTableAdapter.Adapter.Update(proagroDataSet.cidade);
        }
    }
}
