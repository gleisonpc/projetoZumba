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

namespace projetoZumba.Views
{
    /// <summary>
    /// Interaction logic for EditarAluno.xaml
    /// </summary>
    public partial class EditarAluno : Window
    {
        private gerjfd_aluno alunoBanco;
        private Alunos alunos;

        public EditarAluno(gerjfd_aluno alunoBanco,Alunos pAlunos)
        {
            InitializeComponent();
            // TODO: Complete member initialization
            this.alunoBanco = alunoBanco;
            this.alunos = pAlunos;
            //Inlcuir valores na comboBox Modalidades
            gerjfdEntities context = new gerjfdEntities();

            //Pegar dados de modalidades adicionais
            var data = from p in context.gerjfd_modalidade select p.modalidade_nome;
            Modalidade.ItemsSource = data.ToList();

            //Pegar dados de problemas de saude
            var data1 = from p in context.gerjfd_aluno select p.aluno_problemaSaude;
            
            DataDeInicio.Text = alunoBanco.aluno_dataInicio.ToString();
            
            //Setar ComboBox modalidade de acordo com amodalidade gravada no cadastro do aluno
            int index = -1;
            foreach (string item in Modalidade.Items)
            {
                index++;
                if (item == alunoBanco.aluno_modalidade.ToString())
                {
                    break;
                } 
            }
            Modalidade.SelectedItem = Modalidade.Items.GetItemAt(index);

            //Modalidades adicionais
            foreach (var text in data)
            {
                CheckBox label = new CheckBox();
                label.Content = text;
                label.Unchecked += new RoutedEventHandler(change_modalidadeAdicional);
                label.Checked += new RoutedEventHandler(change_modalidadeAdicional);
                ModalidadeAdicional.Items.Add(label);
            }
      
            //Setar modalidades adicionais com as gravadas no cadastro do aluno no banco de dados 
            if (alunoBanco.aluno_modalidadeAdicionais != null)
            {
                string[] list = alunoBanco.aluno_modalidadeAdicionais.Split(',');
                foreach (string modalidade in list)
                {
                    foreach (CheckBox chk in ModalidadeAdicional.Items)
                    {
                        if (chk.Content.ToString() == modalidade)
                        {
                            chk.IsChecked = true;
                        }
                    }
                }
            }

            //Setar problemas de saude com as gravadas no cadastro do aluno no banco de dados 
            if (alunoBanco.aluno_problemaSaude != null)
            {
                string[] list = alunoBanco.aluno_problemaSaude.Split(',');
                foreach (string problemadesaude in list)
                {
                    foreach (CheckBox chk in ProblemaDeSaude.Items)
                    {
                        if (chk.Content.ToString() == problemadesaude)
                        {
                            chk.IsChecked = true;
                        }
                    }
                }
            }

            DiaDeVencimento.Text = alunoBanco.aluno_diaVencimento;
            Valor.Text = alunoBanco.aluno_valor.ToString();
            Nome.Text = alunoBanco.aluno_nome;
            Endereco.Text = alunoBanco.aluno_endereco;
            Numero.Text = alunoBanco.aluno_numero;
            Bairro.Text = alunoBanco.aluno_bairro;
            Cidade.Text = alunoBanco.aluno_cidade;
            Rg.Text = alunoBanco.aluno_rg;
            Cpf.Text = alunoBanco.aluno_cpf;
            TelResidencial.Text = alunoBanco.aluno_telResidencial;
            TelComercial.Text = alunoBanco.aluno_telComercial;
            Celular.Text = alunoBanco.aluno_celular;
            DataDeNascimento.Text = alunoBanco.aluno_dataNascimento.ToString();
            Email.Text = alunoBanco.aluno_email;
            NomeDaMae.Text = alunoBanco.aluno_nomeMae;
            TelefoneMae.Text = alunoBanco.aluno_telMae;
            NomeDoPai.Text = alunoBanco.aluno_nomePai; 
            TelefonePai.Text = alunoBanco.aluno_telPai;
            Peso.Text = alunoBanco.aluno_peso;
            Altura.Text = alunoBanco.aluno_altura;
            PressaoArterial.Text = alunoBanco.aluno_pressaoArterial;
            
            //Setar praticouModalidade conforme o dado gravado no banco
            index = -1;
            foreach (ComboBoxItem item in PraticouModalidade.Items)
            {
                index++;
                if (item.Content.ToString() == alunoBanco.aluno_praticouModalidade.ToString())
                {
                    break;
                }
            }
            PraticouModalidade.SelectedItem = PraticouModalidade.Items.GetItemAt(index);

            ModalidadePraticada.Text = alunoBanco.aluno_modalidadePraticada;

            //Setar CirurgiaRecente conforme o dado gravado no banco
            index = -1;
            foreach (ComboBoxItem item in CirurgiaRecente.Items)
            {
                index++;
                if (item.Content.ToString() == alunoBanco.aluno_cirurgia.ToString())
                {
                    break;
                }
            }
            CirurgiaRecente.SelectedItem = CirurgiaRecente.Items.GetItemAt(index);

            //Setar fumante conforme o dado gravado no banco
            index = -1;
            foreach (ComboBoxItem item in Fumante.Items)
            {
                index++;
                if (item.Content.ToString() == alunoBanco.aluno_fumante.ToString())
                {
                    break;
                }
            }
            Fumante.SelectedItem = Fumante.Items.GetItemAt(index);

            //Setar AlergiaMedicamento conforme o dado gravado no banco
            index = -1;
            foreach (ComboBoxItem item in AlergiaMedicamento.Items)
            {
                index++;
                if (item.Content.ToString() == alunoBanco.aluno_alergiaMedicamento.ToString())
                {
                    break;
                }

            }
            AlergiaMedicamento.SelectedItem = AlergiaMedicamento.Items.GetItemAt(index);

            //Setar doencasVasculares conforme o dado gravado no banco
            index = -1;
            foreach (ComboBoxItem item in DoencasCardiovasculares.Items)
            {
                index++;
                if (item.Content.ToString() == alunoBanco.aluno_doencaCardiovascular.ToString())
                {
                    break;
                }
            }
            DoencasCardiovasculares.SelectedItem = DoencasCardiovasculares.Items.GetItemAt(index);
            
            Parentesco.Text = alunoBanco.aluno_parentesco;
            Digital1.Text = alunoBanco.aluno_digital1;
            Digital2.Text = alunoBanco.aluno_digital2;
            ProblemaDeSaudeObs.Text = alunoBanco.aluno_problemaSaudeObs;
            cirurgiaData.Text = alunoBanco.aluno_periodoCirurgiaData.ToString();
            cirurgiaObs.Text = alunoBanco.aluno_cirurgiaObs;
            alergiaMedicamentoObs.Text = alunoBanco.aluno_alergiaMedicamentoObs;

        }

        private void Confirmar_Click(object sender, RoutedEventArgs e)
        {
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

            gerjfdEntities context = new gerjfdEntities();
            gerjfd_aluno data = new gerjfd_aluno()
            {
                aluno_id = alunoBanco.aluno_id,
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
                aluno_periodoCirurgiaData = Convert.ToDateTime(cirurgiaData.Text),
                aluno_cirurgiaObs = cirurgiaObs.Text,
                aluno_alergiaMedicamentoObs = alergiaMedicamentoObs.Text,
            };
            var original = context.gerjfd_aluno.Find(data.aluno_id);
            if (original != null)
            {
                context.Entry(original).CurrentValues.SetValues(data);
            }
            context.SaveChanges();
            alunos.updateAlunos();
            this.Close();
        }

        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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

        private void Excluir_Click(object sender, RoutedEventArgs e)
        {
            gerjfdEntities context = new gerjfdEntities();
            var row = context.gerjfd_aluno.Find(alunoBanco.aluno_id);
            context.gerjfd_aluno.Remove(row);
            context.SaveChanges();
            alunos.updateAlunos();
            this.Close();
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

            Valor.Text = valor.ToString();
        }
    }
}
