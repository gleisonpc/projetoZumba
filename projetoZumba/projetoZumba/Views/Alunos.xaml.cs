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

namespace projetoZumba
{
    /// <summary>
    /// Interaction logic for Alunos.xaml
    /// </summary>
    public partial class Alunos : Window
    {
        AlunosModel alunosModel = new AlunosModel();

        public Alunos()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.updateAlunos();
        }

        private void AlunosNovo_Click(object sender, RoutedEventArgs e)
        {
            NovoAluno alunosDetalhado = new NovoAluno(this);
            alunosDetalhado.Show();
        }

        public void updateAlunos()
        {
            alunosModel.mostrarAlunos(DataGridAlunos);
        }
    }
}
