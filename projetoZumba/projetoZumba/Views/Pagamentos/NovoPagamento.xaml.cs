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

namespace projetoZumba.Views.Pagamentos
{
    /// <summary>
    /// Interaction logic for Mensalidade.xaml
    /// </summary>
    public partial class NovoPagamento : Window
    {
        private PagamentoAluno pagamentoAluno;

        public NovoPagamento(PagamentoAluno pagamentoAluno)
        {
            InitializeComponent();
            this.pagamentoAluno = pagamentoAluno;

            dataPagamento.SelectedDate = DateTime.Today;
            
        }

        private void ComboBoxItem1_Selected(object sender, RoutedEventArgs e)
        {
            //TRATAMENTO PARA TRAZER CASAS APÓS A VIRGULA QUANDO LER O BANCO DE DADOS
            if (!pagamentoAluno.valorAluno.Text.Contains(","))
            {
                valorCobrado.Text = pagamentoAluno.valorAluno.Text + ".00";
            }
            else
            {
                string[] str = pagamentoAluno.valorAluno.Text.Split(',');

                if (str[1].Length == 1)
                {
                    valorCobrado.Text = pagamentoAluno.valorAluno.Text + "0";
                }
                else
                {
                    valorCobrado.Text = pagamentoAluno.valorAluno.Text;
                }

            }

            //PREENCHER VALORES AUTOMATICOS EM CASO DE MENSALIDADE
            valorCobrado.IsEnabled = false;

            //VALIDA SE CAMPO VENCIMENTO ESTÁ PREENCHIDO
           if(pagamentoAluno.diaVencimento.Text != "")
           {
               dataVencimento.SelectedDate = new DateTime(DateTime.Now.Year,DateTime.Now.Month,Convert.ToInt32(pagamentoAluno.diaVencimento.Text));
           }
           else
             dataVencimento.SelectedDate = DateTime.Today;
        }

        private void ComboBoxItem_Selected(object sender, RoutedEventArgs e)
        {
            nCheque.IsEnabled = false;
            nBanco.IsEnabled = false;
            nAgencia.IsEnabled = false;
        }

        private void ComboBoxItem_Selected_1(object sender, RoutedEventArgs e)
        {
            nCheque.IsEnabled = true;
            nBanco.IsEnabled = true;
            nAgencia.IsEnabled = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (dataVencimento.Text != "" && formaPagamento.Text != "")
            {
                //Modalidades adicionais
                string modalidadesAdicionais = "";
                foreach (CheckBox modalidade in pagamentoAluno.modalidadeAdicional.Items)
                {
                    if (modalidade.IsChecked == true)
                    {
                        modalidadesAdicionais += modalidade.Content + ",";
                    }
                }

                //SUBSTITUIR VIRGULA POR PONTO PARA GRAVAR NO BANCO VALOR COBRADO
                if (valorCobrado.Text.Contains(','))
                {
                    valorCobrado.Text.Replace(',', '.');
                }

                //SUBSTITUIRA VIRGULA POR PONTO PARA GRAVAR NO BANCO VALOR RECEBIDO
                if (valorRecebido.Text.Contains(','))
                {
                    valorRecebido.Text.Replace(',', '.');
                }

                //GERAR SITUAÇÃO DO PAGAMENTO
                string status;
                if ((float.Parse(valorCobrado.Text) - float.Parse(valorRecebido.Text)) == 0)
                {
                    status = "FECHADO";
                }
                else
                    status = "ABERTO";

                gerjfdEntities context = new gerjfdEntities();
                gerjfd_pagamento data = new gerjfd_pagamento()
                {
                    pagamento_aluno_id = Convert.ToInt32(pagamentoAluno.codigoAluno.Text),
                    pagamento_tipo = tipoPagamento.Text,
                    pagamento_vencimento = Convert.ToDateTime(dataVencimento.Text),
                    pagamento_modalidade = pagamentoAluno.modalidadeAluno.Text,
                    pagamento_modalidadesAdicionais = modalidadesAdicionais,
                    pagamento_valor = float.Parse(valorCobrado.Text),
                    pagamento_obs = obsPagamento.Text,
                    patamento_formapgt = formaPagamento.Text,
                    pagamento_data = Convert.ToDateTime(dataPagamento.Text),
                    pagamento_valorpgt = float.Parse(valorRecebido.Text),
                    pagamento_ncheque = nCheque.Text,
                    pagamento_nbanco = nBanco.Text,
                    pagamento_nagencia = nAgencia.Text,
                    pagamento_status = status,
                };
                context.gerjfd_pagamento.Add(data);
                context.SaveChanges();
                pagamentoAluno.updatePagamentos();

                reciboPagamento recibo = new reciboPagamento(data);

                recibo.Show();

                this.Close();
            }
            else
                MessageBox.Show("Favor Verificar a Data de Vencimento e a Forma de Pagamento!");
        }

        private void ComboBoxItem2_Selected(object sender, RoutedEventArgs e)
        {
           //Zerar VALOR COBRADO PARA COLOCAR O VALOR DE PAGAMENTO AVULSO
           valorCobrado.Text = "";
                
           //LIBERAR CAMPO PARA INSERIR VALOR COBRADO
           valorCobrado.IsEnabled = true;

           //VALIDA SE CAMPO VENCIMENTO ESTÁ PREENCHIDO
           //dataVencimento.SelectedDate = DateTime.Today;
        }

    }
}
