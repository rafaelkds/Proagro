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
    /// Interaction logic for UCPessoa.xaml
    /// </summary>
    public partial class UCPessoa : UserControl
    {

        private proagroEntities proEnt;

        public UCPessoa()
        {
            proEnt = new proagroEntities();
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            
            cidadeComboBox.ItemsSource = proEnt.cidade.ToList();


            // Do not load your data at design time.
            // if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            // {
            // 	//Load your data here and assign the result to the CollectionViewSource.
            // 	System.Windows.Data.CollectionViewSource myCollectionViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["Resource Key for CollectionViewSource"];
            // 	myCollectionViewSource.Source = your data
            // }
            /*
                        var viewSource = (System.Windows.Data.CollectionViewSource)this.Resources["proagroEntitiespessoaViewSource"];
                        var entities = new proagroEntities();
                        viewSource.Source = entities.pessoa.Where(x => x.id == 1).ToList();
            */
            //entities.Sa
            // Do not load your data at design time.
            // if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            // {
            // 	//Load your data here and assign the result to the CollectionViewSource.
            // 	System.Windows.Data.CollectionViewSource myCollectionViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["Resource Key for CollectionViewSource"];
            // 	myCollectionViewSource.Source = your data
            // }
            // Do not load your data at design time.
            // if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            // {
            // 	//Load your data here and assign the result to the CollectionViewSource.
            // 	System.Windows.Data.CollectionViewSource myCollectionViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["Resource Key for CollectionViewSource"];
            // 	myCollectionViewSource.Source = your data
            // }
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
            var consulta = new Consulta.Pessoa();
            consulta.ShowDialog();
            long id = consulta.retornaSelecao();
            if (id > 0)
            {
                System.Windows.Data.CollectionViewSource proagroEntitiesViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("proagroEntitiespessoaViewSource")));
                proagroEntitiesViewSource.Source = proEnt.pessoa.Where(x => x.id == id).ToList();
            }
        }

        private void btGravar_Click(object sender, RoutedEventArgs e)
        {
            if (((System.Windows.Data.CollectionViewSource)(this.FindResource("proagroEntitiespessoaViewSource"))).Source == null)
            {
                
                var p = new pessoa()
                {
                    bairro = bairroTextBox.Text,
                    cep = cepTextBox.Text,
                    cidade = (long)cidadeComboBox.SelectedValue,
                    complemento = complementoTextBox.Text,
                    cpf_cnpj = cpf_cnpjTextBox.Text,
                    culturas_areas = culturas_areasTextBox.Text,
                    email = emailTextBox.Text,
                    inscricao_estadual = inscricao_estadualTextBox.Text,
                    nome = nomeTextBox.Text,
                    numero = numeroTextBox.Text,
                    outras_informacoes = outras_informacoesTextBox.Text,
                    rua = ruaTextBox.Text,
                    telefone = telefoneTextBox.Text
                };
                proEnt.pessoa.Add(p);
            }
            proEnt.SaveChanges();
            
        }

        private void btNovo_Click(object sender, RoutedEventArgs e)
        {
            ((System.Windows.Data.CollectionViewSource)(this.FindResource("proagroEntitiespessoaViewSource"))).Source = null;
        }

        private void cepTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            /*
            if(char.IsDigit(e.Key.ToString()[0]))
            {
                if (cepTextBox.Text.Length < 5) cepTextBox.Text += e.Key;
            }
            cepTextBox.Text = string.Format("{0:0}-{1:0}", cepTextBox.Text.Substring(0, 5), cepTextBox.Text.Substring(0, 3))
            e.Handled = true;*/
        }
    }
}
