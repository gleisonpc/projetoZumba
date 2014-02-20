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
    /// Interaction logic for AlunosDetalhado.xaml
    /// </summary>
    public partial class NovoAluno : Window
    {
        private Alunos alunos;

        public NovoAluno(Alunos pAlunos)
        {
            InitializeComponent();
            alunos = pAlunos;
        }

        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Confirmar_Click(object sender, RoutedEventArgs e)
        {
            gerjfdEntities context = new gerjfdEntities();
            gerjfd_aluno data = new gerjfd_aluno()
            {
                aluno_dataInicio = Convert.ToDateTime(DataDeInicio.Text),
                aluno_modalidade = Modalidade.Text,
                aluno_diaVencimento = DiaDeVencimento.Text,
                aluno_valor = Double.Parse(Valor.Text),
                aluno_nome = Nome.Text,
                aluno_endereco = Endereco.Text,
                aluno_numero = Numero.Text,
                aluno_bairro = Bairro.Text,
                aluno_cidade = Cidade.Text,
                aluno_rg = Rg.Text,
                aluno_cpf = Cpf.Text,
                aluno_telResidencial = TelResidencial.Text,
                aluno_telComercial = TelComercial.Text,
                aluno_celular = Celular.Text,
                aluno_dataNacimento = Convert.ToDateTime(DataDeNascimento.Text),
                aluno_email = Email.Text,
                aluno_nomeMae = NomeDaMae.Text,
                aluno_telMae = TelefoneMae.Text,
                aluno_nomePai = NomeDoPai.Text, 
                aluno_telPai = TelefonePai.Text,
                aluno_peso = Peso.Text,
                aluno_altura = Altura.Text,
                aluno_pressaoArterial = PressaoArterial.Text,
                aluno_praticouModalidade = PraticouModalidade.Text,
                aluno_modalidadePraticada = ModalidadePraticada.Text,
                aluno_problemaSaude = ProblemaDeSaude.Text,
                aluno_cirurgia = CirurgiaRecente.Text,
                aluno_fumante = Fumante.Text,
                aluno_alergiaMedicamento = AlergiaMedicamento.Text,
                aluno_doencaCardiovascular = DoencasCardiovasculares.Text,
                aluno_parentesco = Parentesco.Text,
                aluno_digital1 = Digital1.Text,
                aluno_digital2 = Digital2.Text,
            };
            context.gerjfd_aluno.Add(data);
            context.SaveChanges();
            alunos.updateAlunos();
            this.Close();
        }

        private void CadastrarDigital1_Click(object sender, RoutedEventArgs e)
        {
            leitorDig leitor = new leitorDig(Digital1);
            leitor.Show();
        }

        private void CadastrarDigital2_Click(object sender, RoutedEventArgs e)
        {
            leitorDig leitor = new leitorDig(Digital2);
            leitor.Show();
        }
    }
}