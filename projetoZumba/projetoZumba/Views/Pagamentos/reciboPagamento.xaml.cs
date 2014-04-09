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

namespace projetoZumba.Views.Pagamentos
{
    /// <summary>
    /// Interaction logic for reciboPagamento.xaml
    /// </summary>
    public partial class reciboPagamento : Window
    {
        private gerjfd_pagamento data;
        String valorPago;
        String nomeMes;

        public reciboPagamento(gerjfd_pagamento data)
        {
            InitializeComponent();
            this.data = data;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            
            DateTime date = Convert.ToDateTime(data.pagamento_data);
            gerjfdEntities context = new gerjfdEntities();

            var aluno = context.gerjfd_aluno.Find(data.pagamento_aluno_id);

            //TRATAMENTO PARA TRAZER CASAS APÓS A VIRGULA QUANDO LER O BANCO DE DADOS
            if (!Convert.ToString(data.pagamento_valorpgt).Contains(","))
            {
                valorPago = Convert.ToString(data.pagamento_valorpgt) + ".00";
            }
            else
            {
                string[] str = Convert.ToString(data.pagamento_valorpgt).Split(',');

                if (str[1].Length == 1)
                {
                    valorPago = Convert.ToString(data.pagamento_valorpgt) + "0";
                }
                else
                {
                    valorPago = Convert.ToString(data.pagamento_valorpgt);
                }

            }

            //CONVERVER NUMERO EM NOME REFERENTE AO MÊS
            if (date.Month == 1)
                nomeMes = "Janeiro";
            else
                if (date.Month == 2)
                    nomeMes = "Fevereiro";
                else
                    if (date.Month == 3)
                        nomeMes = "Março";
                    else
                        if (date.Month == 4)
                            nomeMes = "Abril";
                        else
                            if (date.Month == 5)
                                nomeMes = "Maio";
                            else
                                if (date.Month == 6)
                                    nomeMes = "Junho";
                                else
                                    if (date.Month == 7)
                                        nomeMes = "Julho";
                                    else
                                        if (date.Month == 8)
                                            nomeMes = "Agosto";
                                        else
                                            if (date.Month == 9)
                                                nomeMes = "Setembro";
                                            else
                                                if (date.Month == 10)
                                                    nomeMes = "Outubro";
                                                else
                                                    if (date.Month == 11)
                                                        nomeMes = "Novembro";
                                                    else
                                                        if (date.Month == 12)
                                                            nomeMes = "Dezembro";

            //VERIFICA SE A CONDIÇÃO DE PAGAMENTO É CHEQUE OU DINHEIRO
            if (data.patamento_formapgt == "Cheque")
            {
                Impressora.imprimir("                                                                  RECIBO" +
                                "\n\n" + "                         Recebi de " + aluno.aluno_nome + " portador do " +
                                " RG sob nº " + aluno.aluno_rg + " matriculado neste Studio, a " +
                                " importância de R$ " + valorPago + "," +
                                " representado pelo cheque nº " + data.pagamento_ncheque + " Banco nº " + data.pagamento_nbanco +
                                " agência nº " + data.pagamento_nagencia + ", ou em dinheiro, cujo pagamento " +
                                " refere-se mensalidade do mês de " + nomeMes + "/2014. " +
                                "\n\n\n                    Por ser verdade, firmamos o presente recibo. " +
                                "\n\n\n                   Mogi Guaçu, " + date.Day + " de " + nomeMes + " de " + date.Year + "." +
                                "\n\n\n\n\n                                              STUDIO JOYCE MARINA FITNESS E DANÇA" +
                                "\n\n\n\n\n________________________________________________________________\n\n\n" +
                                "\n\n                                                                  RECIBO" +
                                "\n\n" + "                         Recebi de " + aluno.aluno_nome + " portador do " +
                                " RG sob nº " + aluno.aluno_rg + " matriculado neste Studio, a " +
                                " importância de R$ " + valorPago + "," +
                                " representado pelo cheque nº " + data.pagamento_ncheque + " Banco nº " + data.pagamento_nbanco +
                                " agência nº " + data.pagamento_nagencia + ", ou em dinheiro, cujo pagamento " +
                                " refere-se mensalidade do mês de " + nomeMes + "/2014. " +
                                "\n\n                    Por ser verdade, firmamos o presente recibo. " +
                                "\n\n                    Mogi Guaçu, " + date.Day + " de " + nomeMes + " de " + date.Year + "." + 
                                "\n\n\n\n\n                                              STUDIO JOYCE MARINA FITNESS E DANÇA");
            }
            else
                {
                    Impressora.imprimir("                                                                  RECIBO" +
                                "\n\n" + "                         Recebi de " + aluno.aluno_nome + " portador do " +
                                " RG sob nº " + aluno.aluno_rg + " matriculado neste Studio, a " +
                                " importância de R$ " + valorPago + "," +
                                " representado em dinheiro, cujo pagamento " +
                                " refere-se mensalidade do mês de " + nomeMes + "/2014. " +
                                "\n\n\n                    Por ser verdade, firmamos o presente recibo. " +
                                "\n\n\n                   Mogi Guaçu, " + date.Day + " de " + nomeMes + " de " + date.Year + "." +
                                "\n\n\n\n\n                                              STUDIO JOYCE MARINA FITNESS E DANÇA" +
                                "\n\n\n\n\n________________________________________________________________\n\n\n" +
                                "\n\n                                                                  RECIBO" +
                                "\n\n" + "                         Recebi de " + aluno.aluno_nome + " portador do " +
                                " RG sob nº " + aluno.aluno_rg + " matriculado neste Studio, a " +
                                " importância de R$ " + valorPago + "," +
                                " representado em dinheiro, cujo pagamento " +
                                " refere-se mensalidade do mês de " + nomeMes + "/2014. " +
                                "\n\n\n                    Por ser verdade, firmamos o presente recibo. " +
                                "\n\n\n                    Mogi Guaçu, " + date.Day + " de " + nomeMes + " de " + date.Year + "." +
                                "\n\n\n\n                                              STUDIO JOYCE MARINA FITNESS E DANÇA");
                }


            this.Close();
        }
    }
}
