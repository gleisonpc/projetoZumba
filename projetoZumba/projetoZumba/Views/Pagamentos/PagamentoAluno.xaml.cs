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
using projetoZumba.Views.Pagamentos;

namespace projetoZumba.Views.Pagamentos
{
    /// <summary>
    /// Interaction logic for PagamentoAluno.xaml
    /// </summary>
    public partial class PagamentoAluno : Window
    {
        private gerjfd_aluno alunoBanco;
        private Pagamento pagamento;
        PagamentosModel pagamentoModel = new PagamentosModel();

        public PagamentoAluno(gerjfd_aluno alunoBanco, Pagamento pagamento)
        {
            InitializeComponent();

            // TODO: Complete member initialization
            this.alunoBanco = alunoBanco;
            this.pagamento = pagamento;

            //Inlcuir valores na Modalidades Principal
            gerjfdEntities context = new gerjfdEntities();
            var data = from p in context.gerjfd_modalidade select p.modalidade_nome;
            modalidadeAluno.ItemsSource = data.ToList();
            try
            {
                modalidadeAluno.SelectedItem = modalidadeAluno.Items.GetItemAt(0);
            }
            catch { }

            //Modalidades adicionais Combobox
            foreach (var text in data)
            {
                CheckBox label = new CheckBox();
                label.Content = text;
                modalidadeAdicional.Items.Add(label);
            }

            //Setar Modalidade de acordo com amodalidade gravada no cadastro do aluno
            int index = -1;
            foreach (string item in modalidadeAluno.Items)
            {
                index++;
                if (item == alunoBanco.aluno_modalidade.ToString())
                {
                    break;
                }
            }
            modalidadeAluno.SelectedItem = modalidadeAluno.Items.GetItemAt(index);

            //Setar modalidades adicionais com as gravadas no cadastro do aluno no banco de dados 
            if (alunoBanco.aluno_modalidadeAdicionais != null)
            {
                string[] list = alunoBanco.aluno_modalidadeAdicionais.Split(',');
                foreach (string modalidade in list)
                {
                    foreach (CheckBox chk in modalidadeAdicional.Items)
                    {
                        if (chk.Content.ToString() == modalidade)
                        {
                            chk.IsChecked = true;
                        }
                    }
                }
            }

            //TRATAMENTO PARA TRAZER CASAS APÓS A VIRGULA QUANDO LER O BANCO DE DADOS
            if (!alunoBanco.aluno_valor.ToString().Contains(","))
            {
                valorAluno.Text = alunoBanco.aluno_valor.ToString() + ".00";
            }
            else
            {
                string[] str = alunoBanco.aluno_valor.ToString().Split(',');

                if (str[1].Length == 1)
                {
                    valorAluno.Text = alunoBanco.aluno_valor.ToString() + "0";
                }
                else
                {
                    valorAluno.Text = alunoBanco.aluno_valor.ToString();
                }

            }

            valorAluno.Text = valorAluno.Text.Replace('.', ',');

            //PREENCHENDO VALORES DE ACORDO COM O CADASTRO
            codigoAluno.Text = alunoBanco.aluno_id.ToString();
            nomeAluno.Text = alunoBanco.aluno_nome;
            diaVencimento.Text = alunoBanco.aluno_diaVencimento;
            pagamentoModel.mostrarPagamentos(dataGridPagamento, alunoBanco.aluno_id);
            situacaoAluno.Text = alunoBanco.aluno_status;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NovoPagamento novopagamento = new NovoPagamento(this);
            novopagamento.Show();
        }

        public void updatePagamentos()
        {
            pagamentoModel.mostrarPagamentos(dataGridPagamento, alunoBanco.aluno_id);
        }

    }
}
