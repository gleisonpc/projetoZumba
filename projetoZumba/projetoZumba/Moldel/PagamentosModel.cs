using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace projetoZumba.Moldel
{
    class PagamentosModel
    {
        internal void mostrarAlunos(System.Windows.Controls.DataGrid DataGridAlunos)
        {
            gerjfdEntities context = new gerjfdEntities();

            //SELECT PARA ENCONTRAR PESSOAS QUE TEM PAGAMENTOS LANÇADAS NO SISTEMA
            var dev = (from p in context.gerjfd_aluno
                       join o in context.gerjfd_pagamento on p.aluno_id equals o.pagamento_aluno_id

                       group new { p, o } by new
                       {
                           p.aluno_id,
                       } into q

                       select new
                       {
                           q.Key.aluno_id,
                           Pagamento = q.Max(x => x.o.pagamento_id),
                       });

            //SELECT DA TABELA DE PAGAMENTOS
            var pag = (from p in context.gerjfd_pagamento
                       select new { p.pagamento_id, p.pagamento_vencimento, p.pagamento_data, p.pagamento_status });

            //SELECT DA TABELA DE ALUNOS
            var data = (from p in context.gerjfd_aluno select new { p.aluno_id, p.aluno_nome, p.aluno_diaVencimento });

            foreach (var i in data)
            {
                foreach (var j in dev)
                {
                    if (i.aluno_id == j.aluno_id)
                    {
                        foreach (var k in pag)
                        {
                            if (k.pagamento_id == j.Pagamento)
                            {                            
                                
                                // DATA DE VNECIMENTO DA MENSALIDADE
                                DateTime dt1 = Convert.ToDateTime(k.pagamento_data);
                                // DATA DE HOJE
                                DateTime dt2 = DateTime.Today;
                                TimeSpan resultado = dt1.Subtract(dt2);

                                if (dt1.Year == dt2.Year)
                                {
                                    if (dt1.Month == dt2.Month)
                                    {

                                    }
                                }

                                /*if (resultado.TotalDays <= 0)
                                {
                                    MessageBox.Show("Pago");
                                }
                                else
                                    MessageBox.Show("Devedor");*/

                                MessageBox.Show(resultado.TotalDays.ToString());
                                
                            }
                        }
                    }
                }
            }


            DataGridAlunos.ItemsSource = data.ToList();
        }

        internal void mostrarPagamentos(System.Windows.Controls.DataGrid DataGridPagamentos, int alunoid)
        {
            gerjfdEntities context = new gerjfdEntities();
            var data = (from p in context.gerjfd_pagamento
                        where p.pagamento_aluno_id == alunoid
                        select new { p.pagamento_id, p.pagamento_vencimento, p.pagamento_valor, p.pagamento_data, p.pagamento_valorpgt, p.pagamento_status });
            DataGridPagamentos.ItemsSource = data.ToList();
        }
    }
}
