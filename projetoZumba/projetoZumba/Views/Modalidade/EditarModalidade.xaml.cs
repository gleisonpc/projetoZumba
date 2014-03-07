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

namespace projetoZumba.Views
{
    /// <summary>
    /// Interaction logic for EditarModalidade.xaml
    /// </summary>
    public partial class EditarModalidade : Window
    {
        private Modalidades modalidades;
        private gerjfd_modalidade modalidadeBanco;

        public EditarModalidade(gerjfd_modalidade modalidadeBanco, Modalidades modalidades)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.modalidadeBanco = modalidadeBanco;
            this.modalidades = modalidades;

            string t = modalidadeBanco.modalidade_vlrp.ToString();

            if (!modalidadeBanco.modalidade_vlrp.ToString().Contains(","))
            {
                vlrPrincipal.Text = modalidadeBanco.modalidade_vlrp.ToString() + ".00";
            }
            else
                {
                    //string str[] = modalidadeBanco.modalidade_vlrp.ToString().Split(".");

                }
            
           
            nomeModalidade.Text = modalidadeBanco.modalidade_nome;
            
            vlrAdicional.Text = modalidadeBanco.modalidade_vlra.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            gerjfdEntities context = new gerjfdEntities();
            gerjfd_modalidade data = new gerjfd_modalidade()
            {
                modalidade_nome = nomeModalidade.Text,
                modalidade_vlrp = float.Parse(vlrPrincipal.Text),
                modalidade_vlra = float.Parse(vlrAdicional.Text),


            };

            var original = context.gerjfd_modalidade.Find(data.modalidade_id);
            if (original != null)
            {
                context.Entry(original).CurrentValues.SetValues(data);
            }
            
            context.SaveChanges();
            modalidades.updateModalidades();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
