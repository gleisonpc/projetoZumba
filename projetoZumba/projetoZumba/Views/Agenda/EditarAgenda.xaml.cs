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

namespace projetoZumba.Views.Agenda
{
    /// <summary>
    /// Interaction logic for EditarAgenda.xaml
    /// </summary>
    public partial class EditarAgenda : Window
    {
        private gerjfd_agenda agendaBanco;
        private Agendas agendas;

        public EditarAgenda(gerjfd_agenda agendaBanco, Agendas agendas)
        {
            InitializeComponent();

            // TODO: Complete member initialization
            this.agendaBanco = agendaBanco;
            this.agendas = agendas;

            Modalidade.Text = agendaBanco.gerjfd_modalidade.modalidade_nome;
            diaSemana.Text = agendaBanco.agenda_dia_semana;
            Horario.Text = agendaBanco.agenda_horario;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            gerjfdEntities context = new gerjfdEntities();
            gerjfd_agenda data = new gerjfd_agenda()
            {
                agenda_id = agendaBanco.agenda_id,
                agenda_id_modalidade = agendaBanco.agenda_id_modalidade,
                agenda_dia_semana = agendaBanco.agenda_dia_semana,
                agenda_horario = agendaBanco.agenda_horario,
                agenda_ativa = "N",
            };
            var original = context.gerjfd_agenda.Find(data.agenda_id);
            if (original != null)
            {
                context.Entry(original).CurrentValues.SetValues(data);
            }
            context.SaveChanges();
            agendas.updateAgendas();
            this.Close();
        }

        
    }
}
