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

namespace projetoZumba.Views.Agenda
{
    /// <summary>
    /// Interaction logic for Agenda.xaml
    /// </summary>
    public partial class Agendas : Window
    {
        AgendasModel agendasModel = new AgendasModel();

        public Agendas()
        {
            InitializeComponent();
        }

        private void AgendaNova_Click(object sender, RoutedEventArgs e)
        {
            NovaAgenda novaAgenda = new NovaAgenda(this);
            novaAgenda.Show();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.updateAgendas();
        }

        public void updateAgendas()
        {
            agendasModel.mostrarAgendas(DataGridAgendas);
        }

        private void DataGridAgendas_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            dynamic aluno = DataGridAgendas.SelectedItem;
            if (aluno != null)
            {
                int id = aluno.agenda_id;
                gerjfdEntities context = new gerjfdEntities();

                foreach (gerjfd_agenda agendaBanco in context.gerjfd_agenda)
                {
                    if (agendaBanco.agenda_id == id)
                    {
                        EditarAgenda editarAgenda = new EditarAgenda(agendaBanco, this);
                        editarAgenda.Show();
                    }
                }
            }            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            dynamic agenda = DataGridAgendas.SelectedItem;
            if (agenda != null)
            {
                int id = agenda.agenda_id;
                gerjfdEntities context = new gerjfdEntities();

                foreach (gerjfd_agenda agendaBanco in context.gerjfd_agenda)
                {
                    if (agendaBanco.agenda_id == id)
                    {
                        findAluno chamada = new findAluno(agendaBanco, this);
                        chamada.Show();
                    }
                }
            }            
            

            
            
        }

    }
}
