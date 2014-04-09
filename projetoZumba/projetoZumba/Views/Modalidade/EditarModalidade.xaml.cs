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

namespace projetoZumba.Views.Modalidade
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

            nomeModalidade.Text = modalidadeBanco.modalidade_nome;

            if (!modalidadeBanco.modalidade_vlrp.ToString().Contains(","))
            {
                vlrPrincipal.Text = modalidadeBanco.modalidade_vlrp.ToString() + ".00";
            }
            else
                {
                    string[] str = modalidadeBanco.modalidade_vlrp.ToString().Split(',');

                    if (str[1].Length == 1)
                    {
                        vlrPrincipal.Text = modalidadeBanco.modalidade_vlrp.ToString() + "0";
                    }
                    else
                    {
                        vlrPrincipal.Text = modalidadeBanco.modalidade_vlrp.ToString();
                    }

                }

            if (!modalidadeBanco.modalidade_vlra.ToString().Contains(","))
            {
                vlrAdicional.Text = modalidadeBanco.modalidade_vlra.ToString() + ".00";
            }
            else
            {
                string[] str = modalidadeBanco.modalidade_vlra.ToString().Split(',');

                if (str[1].Length == 1)
                {
                    vlrAdicional.Text = modalidadeBanco.modalidade_vlra.ToString() + "0";
                }
                else
                {
                    vlrAdicional.Text = modalidadeBanco.modalidade_vlra.ToString();
                }

            }

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

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            gerjfdEntities context = new gerjfdEntities();
            var row = context.gerjfd_modalidade.Find(modalidadeBanco.modalidade_id);
            context.gerjfd_modalidade.Remove(row);
            context.SaveChanges();
            modalidades.updateModalidades();
            this.Close();
        }
    }
}
