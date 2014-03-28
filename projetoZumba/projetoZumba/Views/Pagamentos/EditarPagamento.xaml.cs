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
    /// Interaction logic for EditarPagamento.xaml
    /// </summary>
    public partial class EditarPagamento : Window
    {
        private gerjfd_pagamento pagamentoBanco;
        private PagamentoAluno pagamentoAluno;
        private Pagamento pagamento;

        public EditarPagamento(gerjfd_pagamento pagamentoBanco, PagamentoAluno pagamentoAluno)
        {
            InitializeComponent();

            this.pagamentoBanco = pagamentoBanco;

            // TODO: Complete member initialization
            this.pagamentoBanco = pagamentoBanco;
            this.pagamentoAluno = pagamentoAluno;

            //CARREGAR INFORMAÇÕES DO BANCO DE DADOS
            tipoPagamento.Text = pagamentoBanco.pagamento_tipo;
            valorCobrado.Text = pagamentoBanco.pagamento_valor.ToString();
            dataVencimento.Text = pagamentoBanco.pagamento_vencimento.ToString();
            formaPagamento.Text = pagamentoBanco.patamento_formapgt;
            nCheque.Text = pagamentoBanco.pagamento_ncheque;
            nBanco.Text = pagamentoBanco.pagamento_nbanco;
            nAgencia.Text = pagamentoBanco.pagamento_nagencia;

            //TRATAMENTO PARA TRAZER CASAS APÓS A VIRGULA QUANDO LER O BANCO DE DADOS
            if (!pagamentoBanco.pagamento_valorpgt.ToString().Contains("."))
            {
                valorRecebido.Text = pagamentoBanco.pagamento_valorpgt.ToString() + ",00";
            }
            else
            {
                string[] str = pagamentoBanco.pagamento_valorpgt.ToString().Split('.');

                if (str[1].Length == 1)
                {
                    valorRecebido.Text = pagamentoBanco.pagamento_valorpgt.ToString() + "0";
                }
                else
                {
                    valorRecebido.Text = pagamentoBanco.pagamento_valorpgt.ToString();
                }

            }

            dataPagamento.Text = pagamentoBanco.pagamento_data.ToString();
            obsPagamento.Text = pagamentoBanco.pagamento_obs;

            if (tipoPagamento.Text == "Mensalidade")
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

                //BLOQUEAR EDIÇÃO DE CAMPOS DE VALOR DE MENSALIDADE E VENCIMENTO
                valorCobrado.IsEnabled = false;
                
            }
            else
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

                //PERMITE A EDIÇÃO DO CAMPO DO VALOR
                valorCobrado.IsEnabled = true;
                
            }
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
            gerjfdEntities context = new gerjfdEntities();
            var row = context.gerjfd_pagamento.Find(pagamentoBanco.pagamento_id);
            context.gerjfd_pagamento.Remove(row);
            context.SaveChanges();
            pagamentoAluno.updatePagamentos();
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
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
                pagamento_id = pagamentoBanco.pagamento_id,
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
                pagamento_nbanco  = nBanco.Text,
                pagamento_nagencia = nAgencia.Text,
                pagamento_status  = status,
            };
           
            var original = context.gerjfd_pagamento.Find(data.pagamento_id);
            if (original != null)
            {
                context.Entry(original).CurrentValues.SetValues(data);
            }

            
            

            context.SaveChanges();
            pagamentoAluno.updatePagamentos();
            this.Close();
        }

    }
}
