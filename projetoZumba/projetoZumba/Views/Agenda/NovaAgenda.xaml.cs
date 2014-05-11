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
    /// Interaction logic for NovaAgenda.xaml
    /// </summary>
    public partial class NovaAgenda : Window
    {

        Agendas agendas;

        public NovaAgenda(Agendas agendas)
        {
            InitializeComponent();

            this.agendas = agendas;

            //Inlcuir valores na Modalidades Principal
            gerjfdEntities context = new gerjfdEntities();
            var data = context.gerjfd_modalidade.Select(x => new { x.modalidade_id, x.modalidade_nome });
            List<String> modalidade = new List<String>();
            foreach(var t in data){

                String modalidades = t.modalidade_id.ToString() + " - " + t.modalidade_nome;
                modalidade.Add(modalidades);

            }

            Modalidade.ItemsSource = modalidade.ToList();
            try
            {
                Modalidade.SelectedItem = Modalidade.Items.GetItemAt(0);
            }
            catch { }

            //SELECIONAR VALORES PARA INICIALIZAÇÃO
            diaSemana.SelectedItem = diaSemana.Items.GetItemAt(0);
            horario.SelectedItem = horario.Items.GetItemAt(0);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            gerjfdEntities context = new gerjfdEntities();

            String[] modalidade = Modalidade.Text.Split();

            gerjfd_agenda data = new gerjfd_agenda()
            {
                agenda_id_modalidade = Convert.ToInt32(modalidade[0]),
                agenda_dia_semana = diaSemana.Text,
                agenda_horario = horario.Text,
                agenda_ativa = "Y",
            };
            context.gerjfd_agenda.Add(data);
            
            ////SALVA INFORMAÇÕES NO BANCO DE DADOS E ATUALIZA TELA ALUNOS
            context.SaveChanges();
            agendas.updateAgendas();
        }
    }
}
