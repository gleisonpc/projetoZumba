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

namespace projetoZumba.Views.Pagamentos
{
    /// <summary>
    /// Interaction logic for Pagamento.xaml
    /// </summary>
    public partial class Pagamento : Window
    {

        PagamentosModel pagamentoModel = new PagamentosModel();

        public Pagamento()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.updateAlunos();

        }

        public void updateAlunos()
        {
            pagamentoModel.mostrarAlunos(DataGridPagamento);
        }

        private void DataGridPagamento_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            dynamic aluno = DataGridPagamento.SelectedItem;
            if (aluno != null)
            {
                int id = aluno.aluno_id;
                gerjfdEntities context = new gerjfdEntities();

                foreach (gerjfd_aluno alunoBanco in context.gerjfd_aluno)
                {
                    if (alunoBanco.aluno_id == id)
                    {
                        PagamentoAluno pagamentoAluno = new PagamentoAluno(alunoBanco, this);
                        pagamentoAluno.Show();
                    }
                }
            }            
            
        }
    }
}
