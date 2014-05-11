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

namespace projetoZumba.Views.Aluno
{
    /// <summary>
    /// Interaction logic for AlertaNome.xaml
    /// </summary>
    public partial class AlertaNome : Window
    {
        private NovoAluno novoAluno;
        private Alunos alunos;

        public AlertaNome(NovoAluno novoAluno, Alunos alunos)
        {
            InitializeComponent();

            // TODO: Complete member initialization
            this.novoAluno = novoAluno;
            this.alunos = alunos;
        }

        //CANCELAR INSERSÃO COM O MESMO NOME
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            novoAluno.Close();
            this.Close();
        }

        //CONFIRMA PARA INSERIR ALUNO COM O MESMO NOME
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //Modalidades adicionais
            string modalidadesAdicionais = "";
            foreach (CheckBox modalidade in novoAluno.ModalidadeAdicional.Items)
            {
                if (modalidade.IsChecked == true)
                {
                    modalidadesAdicionais += modalidade.Content + ",";
                }
            }

            //Problemas de Saúde
            string problemasdeSaude = "";
            foreach (CheckBox problemas in novoAluno.ProblemaDeSaude.Items)
            {
                if (problemas.IsChecked == true)
                {
                    problemasdeSaude += problemas.Content + ",";
                }
            }

            //CARREGAR INFORMAÇÕES PARA GRAVAR NO BANCO
            gerjfdEntities context = new gerjfdEntities();
            gerjfd_aluno data = new gerjfd_aluno()
            {
                aluno_dataInicio = Convert.ToDateTime(novoAluno.DataDeInicio.Text),
                aluno_modalidade = novoAluno.Modalidade.SelectedItem.ToString(),
                aluno_diaVencimento = novoAluno.DiaDeVencimento.Text,
                aluno_valor = Double.Parse(novoAluno.Valor.Text),
                aluno_nome = novoAluno.Nome.Text,
                aluno_endereco = novoAluno.Endereco.Text,
                aluno_numero = novoAluno.Numero.Text,
                aluno_bairro = novoAluno.Bairro.Text,
                aluno_cidade = novoAluno.Cidade.Text,
                aluno_rg = novoAluno.Rg.Text,
                aluno_cpf = novoAluno.Cpf.Text,
                aluno_telResidencial = novoAluno.TelResidencial.Text,
                aluno_telComercial = novoAluno.TelComercial.Text,
                aluno_celular = novoAluno.Celular.Text,
                aluno_dataNascimento = Convert.ToDateTime(novoAluno.DataDeNascimento.Text),
                aluno_email = novoAluno.Email.Text,
                aluno_nomeMae = novoAluno.NomeDaMae.Text,
                aluno_telMae = novoAluno.TelefoneMae.Text,
                aluno_nomePai = novoAluno.NomeDoPai.Text,
                aluno_telPai = novoAluno.TelefonePai.Text,
                aluno_peso = novoAluno.Peso.Text,
                aluno_altura = novoAluno.Altura.Text,
                aluno_pressaoArterial = novoAluno.PressaoArterial.Text,
                aluno_praticouModalidade = novoAluno.PraticouModalidade.Text,
                aluno_modalidadePraticada = novoAluno.ModalidadePraticada.Text,
                aluno_problemaSaude = problemasdeSaude,
                aluno_cirurgia = novoAluno.CirurgiaRecente.Text,
                aluno_fumante = novoAluno.Fumante.Text,
                aluno_alergiaMedicamento = novoAluno.AlergiaMedicamento.Text,
                aluno_doencaCardiovascular = novoAluno.DoencasCardiovasculares.Text,
                aluno_parentesco = novoAluno.Parentesco.Text,
                aluno_digital1 = novoAluno.Digital1.Text,
                aluno_digital2 = novoAluno.Digital2.Text,
                aluno_modalidadeAdicionais = modalidadesAdicionais,
                aluno_problemaSaudeObs = novoAluno.ProblemaDeSaudeObs.Text,
                aluno_cirurgiaObs = novoAluno.cirurgiaObs.Text,
                aluno_alergiaMedicamentoObs = novoAluno.alergiaMedicamentoObs.Text,
                aluno_status = "OK",
            };
            context.gerjfd_aluno.Add(data);

            //LIMPAR FORMULÁRIO APÓS GRAVAR NO BANCO
            novoAluno.DataDeInicio.Text = DateTime.Today.ToString();
            novoAluno.Modalidade.SelectedIndex = 0;
            novoAluno.DiaDeVencimento.Text = "";
            novoAluno.Nome.Text = "";
            novoAluno.Endereco.Text = "";
            novoAluno.Numero.Text = "";
            novoAluno.Bairro.Text = "";
            novoAluno.Cidade.Text = "";
            novoAluno.Rg.Text = "";
            novoAluno.Cpf.Text = "";
            novoAluno.TelResidencial.Text = "";
            novoAluno.TelComercial.Text = "";
            novoAluno.Celular.Text = "";
            novoAluno.DataDeNascimento.Text = DateTime.Today.ToString();
            novoAluno.Email.Text = "";
            novoAluno.NomeDaMae.Text = "";
            novoAluno.TelefoneMae.Text = "";
            novoAluno.CelularMae.Text = "";
            novoAluno.NomeDoPai.Text = "";
            novoAluno.TelefonePai.Text = "";
            novoAluno.CelularPai.Text = "";
            novoAluno.Peso.Text = "";
            novoAluno.Altura.Text = "";
            novoAluno.PressaoArterial.Text = "";
            novoAluno.PraticouModalidade.SelectedIndex = 0;
            novoAluno.ModalidadePraticada.Text = "";

            foreach (CheckBox b in novoAluno.ProblemaDeSaude.Items)
            {
                b.IsChecked = false;
            }

            foreach (CheckBox a in novoAluno.ModalidadeAdicional.Items)
            {
                a.IsChecked = false;
            }

            novoAluno.ProblemaDeSaudeObs.Text = "";
            novoAluno.CirurgiaRecente.SelectedIndex = 1;
            novoAluno.cirurgiaObs.Text = "";
            novoAluno.Fumante.SelectedIndex = 1;
            novoAluno.AlergiaMedicamento.SelectedIndex = 1;
            novoAluno.alergiaMedicamentoObs.Text = "";
            novoAluno.DoencasCardiovasculares.SelectedIndex = 1;
            novoAluno.Parentesco.Text = "";
            novoAluno.Digital1.Text = "";
            novoAluno.Digital2.Text = "";

            //SALVA INFORMAÇÕES NO BANCO DE DADOS E ATUALIZA TELA ALUNOS
            context.SaveChanges();
            alunos.updateAlunos();

            this.Close();
        }
    }
}
