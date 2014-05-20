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
using projetoZumba.Lib;
using projetoZumba.Views;
using projetoZumba.Views.Aluno;
using projetoZumba.Views.Modalidade;
using projetoZumba.Views.Pagamentos;
using projetoZumba.Views.Agenda;
using Microsoft.Win32;
using System.Diagnostics;

namespace projetoZumba.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
 
        public MainWindow()
        {
            InitializeComponent();

            alertDev alerta = new alertDev();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Alunos alunos = new Alunos();
            alunos.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Modalidades modalidades = new Modalidades();
            modalidades.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Agendas agendas = new Agendas();
            agendas.Show();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            SaveFileDialog backupWay = new SaveFileDialog();

            backupWay.InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            backupWay.Filter = "Todos (*.*)|*.*|Backup SQL (*.bak)|*.bak";

            backupWay.ShowDialog();

            String str;
            str = "/C Sqlcmd -S .\\sqlexpress -U sa -P gleison23 -Q \"EXEC gerjfd.dbo.gerjfd_BACKUP @caminho ='" + backupWay.FileName + ".bak'\"";

            Process.Start("cmd", str);

        }

    }
}
