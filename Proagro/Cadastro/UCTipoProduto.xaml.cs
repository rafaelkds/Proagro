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
    /// Interaction logic for UCTipoProduto.xaml
    /// </summary>
    public partial class UCTipoProduto : UserControl
    {
        private proagroEntities proEnt;

        public UCTipoProduto()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

            // Do not load your data at design time.
            // if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            // {
            // 	//Load your data here and assign the result to the CollectionViewSource.
            // 	System.Windows.Data.CollectionViewSource myCollectionViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["Resource Key for CollectionViewSource"];
            // 	myCollectionViewSource.Source = your data
            // }
            System.Windows.Data.CollectionViewSource proagroEntitiesViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("proagroEntitiestipo_produtoViewSource")));
            // Load data by setting the CollectionViewSource.Source property:

            proEnt = new proagroEntities();
            proagroEntitiesViewSource.Source = proEnt.tipo_produto.ToList();

            tipo_produtoDataGrid.SelectedIndex = -1;
        }

        private void btGravar_Click(object sender, RoutedEventArgs e)
        {
            if(tipo_produtoDataGrid.SelectedIndex == -1)
            {
                proEnt.tipo_produto.Add(new tipo_produto()
                {
                    descricao = descricaoTextBox.Text
                });
            }
            proEnt.SaveChanges();

            UserControl_Loaded(null, null);
        }

        private void btNovo_Click(object sender, RoutedEventArgs e)
        {
            tipo_produtoDataGrid.SelectedIndex = -1;
        }
    }
}
