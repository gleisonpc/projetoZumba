using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace projetoZumba
{
    class AlunosModel
    {
        internal void mostrarAlunos(System.Windows.Controls.DataGrid DataGridAlunos)
        {
            gerjfdEntities context = new gerjfdEntities();
            var data = (from p in context.gerjfd_aluno 
                        select new { p.aluno_id, p.aluno_nome, p.aluno_diaVencimento, p.aluno_status });
            DataGridAlunos.ItemsSource = data.ToList();
        }

        //REALIZA A BUSCA NO BANCO DE DADOS DE ACORDO COM O CAMPO BUSCA DIGITADO PELO USUARIO
        internal void mostrarBuscaAlunos(System.Windows.Controls.DataGrid DataGridAlunos, String Busca, String Tipo)
        {
            gerjfdEntities context = new gerjfdEntities();
            var id = new int();

            if (Tipo == "Código")
            {

                id = int.Parse(Busca);

            }

            //CARREGA VARIAVEIS COM O RETORNO DA BUSCA
            var data1 = (from p in context.gerjfd_aluno
                         where p.aluno_nome.StartsWith(Busca)

                         select new { p.aluno_id, p.aluno_nome, p.aluno_diaVencimento, p.aluno_status });

            var data2 = (from p in context.gerjfd_aluno
                         where p.aluno_id == id

                         select new { p.aluno_id, p.aluno_nome, p.aluno_diaVencimento, p.aluno_status });

            var data3 = (from p in context.gerjfd_aluno
                         where p.aluno_status.StartsWith(Busca)

                         select new { p.aluno_id, p.aluno_nome, p.aluno_diaVencimento, p.aluno_status });

            //LOGICA PARA MOSTRAR A BUSCA NO DATAGRID
            if (Tipo == "Nome")
            {
                try
                {
                    DataGridAlunos.ItemsSource = data1.ToList();
                }
                catch { }
            }
            else
                if (Tipo == "Código")
                {
                    try
                    {
                        DataGridAlunos.ItemsSource = data2.ToList();
                    }
                    catch (Exception Err) { MessageBox.Show(Err.ToString()); }
                }
                else
                    if (Tipo == "Status")
                    {
                        try
                        {
                            DataGridAlunos.ItemsSource = data3.ToList();
                        }
                        catch { }
                    }


        }
    }
}
