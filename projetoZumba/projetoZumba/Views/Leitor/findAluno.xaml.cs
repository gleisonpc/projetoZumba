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
using projetoZumba.Lib;

namespace projetoZumba.Views
{

    
    /// <summary>
    /// Interaction logic for findAluno.xaml
    /// </summary>
    public partial class findAluno : Window
    {

        private leitorVer leitor;
        private Agenda.Agendas agendas;
        private gerjfd_agenda agendaBanco;

        public findAluno(gerjfd_agenda agendaBanco, Agenda.Agendas agendas)
        {
            InitializeComponent();

            // TODO: Complete member initialization
            this.agendaBanco = agendaBanco;
            this.agendas = agendas;

            buttoncancel.IsEnabled = true;
            buttonok.IsEnabled = true;
            buttonver.IsEnabled = true;

            leitor = new leitorVer(this);

            idAgenda.Text = agendaBanco.agenda_id.ToString();

            leitor.Init();
            leitor.Start();
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            leitor.Stop();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            leitor.Stop();
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            idVerif.Text = "";
            nomeVerif.Content = "";
            verStatus.Content = "";
            varVerif.Content = "";

            leitor = new leitorVer(this);

            leitor.Init();
            leitor.Start();
        }

        private void idVerif_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (idVerif.Text == "ALUNO NÃO ENCONTRADO" || idVerif.Text == "")
            {
                //MessageBox.Show("teste");
                //idVerif.Text = "";
            }
            else
            {
                gerjfdEntities context = new gerjfdEntities();

                gerjfd_frequencia data = new gerjfd_frequencia()
                {
                    frequencia_aluno_id = Convert.ToInt32(idVerif.Text),
                    frequencia_id_agenda = Convert.ToInt32(idAgenda.Text),
                    frequencia_data = DateTime.Now,
                };
                context.gerjfd_frequencia.Add(data);
                context.SaveChanges();
                //this.Close();
            }
        }   
    
    }
}
