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

//Comentario

namespace projetoZumba.Views.Aluno
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

            //Inlcuir valores na Modalidades Principal
            gerjfdEntities context = new gerjfdEntities();
            var data = from p in context.gerjfd_modalidade select p.modalidade_nome;
            Modalidade.ItemsSource = data.ToList();
            try
            {
                Modalidade.SelectedItem = Modalidade.Items.GetItemAt(0);
            }
            catch { }

            //Modalidades adicionais Combobox
            foreach (var text in data)
            {
                CheckBox label = new CheckBox();
                label.Content = text;
                label.Unchecked += new RoutedEventHandler(change_modalidadeAdicional);
                label.Checked += new RoutedEventHandler(change_modalidadeAdicional);
                ModalidadeAdicional.Items.Add(label);
            }
            
            //Data de inicio 
            DataDeInicio.SelectedDate = DateTime.Today;

            //Data de nacimento 
            DataDeNascimento.SelectedDate = DateTime.Today;

            //Modalidade esportiva
            PraticouModalidade.SelectedItem = PraticouModalidade.Items.GetItemAt(0);

            //Cirurgia
            CirurgiaRecente.SelectedItem = CirurgiaRecente.Items.GetItemAt(1);

            //Fumante
            Fumante.SelectedItem = Fumante.Items.GetItemAt(1);

            //Alergia Medicamento
            AlergiaMedicamento.SelectedItem = AlergiaMedicamento.Items.GetItemAt(1);

            //Doencas Cardiovasculares
            DoencasCardiovasculares.SelectedItem = DoencasCardiovasculares.Items.GetItemAt(1);
        }

        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Confirmar_Click(object sender, RoutedEventArgs e)
        {
            bool nomeExiste = false;
            gerjfdEntities context = new gerjfdEntities();

            //Modalidades adicionais
            string modalidadesAdicionais = "";
            foreach (CheckBox modalidade in ModalidadeAdicional.Items)
            {
                if (modalidade.IsChecked == true)
                {
                    modalidadesAdicionais += modalidade.Content + ",";
                }
            }

            //Problemas de Saúde
            string problemasdeSaude = "";
            foreach (CheckBox problemas in ProblemaDeSaude.Items)
            {
                if (problemas.IsChecked == true)
                {
                    problemasdeSaude += problemas.Content + ",";
                }
            }

            //VERIFICA SE EXISTE O MESMO NOME JÁ CADASTRADO
            var nome = context.gerjfd_aluno.Select(x => x.aluno_nome);
            AlertaNome alerta = new AlertaNome(this, alunos);

            foreach(var n in nome){

                if (n.ToString() == Nome.Text)
                {
                    nomeExiste = true; 
                }
                
            }


            if (nomeExiste)
            {
                alerta.Show();
            }
            else
            {
                gerjfd_aluno data = new gerjfd_aluno()
                {
                    aluno_dataInicio = Convert.ToDateTime(DataDeInicio.Text),
                    aluno_modalidade = Modalidade.SelectedItem.ToString(),
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
                    aluno_dataNascimento = Convert.ToDateTime(DataDeNascimento.Text),
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
                    aluno_problemaSaude = problemasdeSaude,
                    aluno_cirurgia = CirurgiaRecente.Text,
                    aluno_fumante = Fumante.Text,
                    aluno_alergiaMedicamento = AlergiaMedicamento.Text,
                    aluno_doencaCardiovascular = DoencasCardiovasculares.Text,
                    aluno_parentesco = Parentesco.Text,
                    aluno_digital1 = Digital1.Text,
                    aluno_digital2 = Digital2.Text,
                    aluno_modalidadeAdicionais = modalidadesAdicionais,
                    aluno_problemaSaudeObs = ProblemaDeSaudeObs.Text,
                    aluno_cirurgiaObs = cirurgiaObs.Text,
                    aluno_alergiaMedicamentoObs = alergiaMedicamentoObs.Text,
                    aluno_status = "OK",
                    aluno_celularMae = CelularMae.Text,
                    aluno_celularPai = CelularPai.Text,
                };
                context.gerjfd_aluno.Add(data);
                
                //this.Close();

                //LIMPAR FORMULÁRIO APÓS GRAVAR NO BANCO
                DataDeInicio.Text = DateTime.Today.ToString();
                Modalidade.SelectedIndex = 0;
                DiaDeVencimento.Text = "";
                Nome.Text = "";
                Endereco.Text = "";
                Numero.Text = "";
                Bairro.Text = "";
                Cidade.Text = "";
                Rg.Text = "";
                Cpf.Text = "";
                TelResidencial.Text = "";
                TelComercial.Text = "";
                Celular.Text = "";
                DataDeNascimento.Text = DateTime.Today.ToString();
                Email.Text = "";
                NomeDaMae.Text = "";
                TelefoneMae.Text = "";
                CelularMae.Text = "";
                NomeDoPai.Text = "";
                TelefonePai.Text = "";
                CelularPai.Text = "";
                Peso.Text = "";
                Altura.Text = "";
                PressaoArterial.Text = "";
                PraticouModalidade.SelectedIndex = 0;
                ModalidadePraticada.Text = "";

                foreach (CheckBox b in ProblemaDeSaude.Items) 
                {
                    b.IsChecked = false;
                }

                foreach (CheckBox a in ModalidadeAdicional.Items)
                {
                    a.IsChecked = false;
                }

                ProblemaDeSaudeObs.Text = "";
                CirurgiaRecente.SelectedIndex = 1;
                cirurgiaObs.Text = "";
                Fumante.SelectedIndex = 1;
                AlergiaMedicamento.SelectedIndex = 1;
                alergiaMedicamentoObs.Text = "";
                DoencasCardiovasculares.SelectedIndex = 1;
                Parentesco.Text = "";
                Digital1.Text = "";
                Digital2.Text = "";

                //SALVA INFORMAÇÕES NO BANCO DE DADOS E ATUALIZA TELA ALUNOS
                context.SaveChanges();
                alunos.updateAlunos();

            }
        }

        private void CadastrarDigital1_Click(object sender, RoutedEventArgs e)
        {

            leitorDig leitura1 = new leitorDig(Digital1);
            leitura1.Show();
         
        }       

        private void CadastrarDigital2_Click(object sender, RoutedEventArgs e)
        {
            leitorDig leitura2 = new leitorDig(Digital2);
            leitura2.Show();
        }

        private void Modalidade_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            calcularValor();
        }

        private void change_modalidadeAdicional(object sender, RoutedEventArgs e)
        {
            calcularValor();
        }

        private void calcularValor()
        {
            float valor = 0;
            gerjfdEntities context = new gerjfdEntities();
            foreach (gerjfd_modalidade modalidade in context.gerjfd_modalidade)
            {
                if (modalidade.modalidade_nome == Modalidade.SelectedItem.ToString())
                {
                    valor = float.Parse(modalidade.modalidade_vlrp.ToString());
                }
            }

            //Calcula modalidades Adicionais 
            foreach (CheckBox modalidade in ModalidadeAdicional.Items)
            {
                if (modalidade.IsChecked == true)
                {
                    foreach (gerjfd_modalidade modalidade2 in context.gerjfd_modalidade)
                    {
                        if (modalidade2.modalidade_nome == modalidade.Content.ToString())
                        {
                            valor += float.Parse(modalidade2.modalidade_vlra.ToString());
                        }
                    }
                }
            }

            if (!valor.ToString().Contains("."))
            {
                Valor.Text = valor.ToString() + ",00";
            }
            else
            {
                string[] str = valor.ToString().Split('.');

                if (str[1].Length == 1)
                {
                    Valor.Text = valor.ToString() + "0";
                }
                else
                {
                    Valor.Text = valor.ToString();
                }

            }

            //Valor.Text = valor.ToString();
        }

    }
}