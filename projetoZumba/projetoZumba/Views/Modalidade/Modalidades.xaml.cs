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
using projetoZumba.Moldel;

namespace projetoZumba.Views
{
    /// <summary>
    /// Interaction logic for Modalidades.xaml
    /// </summary>
    public partial class Modalidades : Window
    {
        ModalidadesModel modalidades = new ModalidadesModel();

        public Modalidades()
        {
            InitializeComponent();
        }

        private void ModalidadeNovo_Click(object sender, RoutedEventArgs e)
        {
            NovaModalidade novamodalidade = new NovaModalidade(this);
            novamodalidade.Show();
        }

        private void DataGridModalidades_Loaded(object sender, RoutedEventArgs e)
        {
            this.updateModalidades();
        }

        public void updateModalidades()
        {
            modalidades.mostrarModalidades(DataGridModalidades);
        }

        private void DataGridModalidades_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            dynamic modalidade = DataGridModalidades.SelectedItem;

            int id = modalidade.modalidade_id;
            gerjfdEntities context = new gerjfdEntities();

            foreach (gerjfd_modalidade modalidadeBanco in context.gerjfd_modalidade)
            {
                if (modalidadeBanco.modalidade_id == id)
                {
                    EditarModalidade editarModalidade = new EditarModalidade(modalidadeBanco, this);
                    editarModalidade.Show();
                }
            }
        }

    }
}