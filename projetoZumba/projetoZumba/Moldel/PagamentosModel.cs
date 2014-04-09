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
        String status;
        int id;
        bool jaDeve = false;

        internal void mostrarAlunos(System.Windows.Controls.DataGrid DataGridAlunos)
        {
            
            gerjfdEntities context = new gerjfdEntities();

            //SELECT PAGAMENTOS EM ABERTO
            var aberto = context.gerjfd_pagamento.Where(x => x.pagamento_status == "Aberto");

            //Select Ultima Mensalidade
            var ultima = context.gerjfd_pagamento.Where(x => x.pagamento_tipo == "Mensalidade").GroupBy(x => x.pagamento_aluno_id).Select(y => new {idAluno = y.Key, vencimento = y.Max(x => x.pagamento_vencimento)});

            //SELECT DA TABELA DE PAGAMENTOS
            var pag = context.gerjfd_pagamento.Select(x => x);

            //SELECT DA TABELA DE ALUNOS
            var data = (from p in context.gerjfd_aluno select p);

            
            //VERIFICAR SE ALUNO TEM PAGAMENTO EM ABERTO
            foreach (var a in data)
            {
                try{
                        foreach (var pa in aberto)
                        {
                            if (a.aluno_id == pa.pagamento_aluno_id)
                            {
                                status = "DEV";
                                jaDeve = true;
                            }
                            else
                                status = "OK";
                        }
                }
                catch{}

                if (!jaDeve)
                {
                    try { 
                            foreach(var u in ultima)
                            {
                                if (u.idAluno == a.aluno_id)
                                {
                                    try
                                    {
                                        foreach (var p in pag)
                                        {
                                            if (u.vencimento == p.pagamento_vencimento && u.idAluno == p.pagamento_aluno_id)
                                            {
                                                // DATA DE VNECIMENTO DA MENSALIDADE
                                                DateTime dt1 = Convert.ToDateTime(p.pagamento_data);
                                                // DATA DE HOJE
                                                DateTime dt2 = DateTime.Today;
                                                TimeSpan resultado = dt2.Subtract(dt1);

                                                //LOGICA PARA VERIFICAR DEVEDORES MENSALIDADES
                                                if (dt1.Year == dt2.Year)
                                                {
                                                    if (dt1.Month == dt2.Month)
                                                    {
                                                        if (DateTime.Today.Month == Convert.ToDateTime(p.pagamento_vencimento).Month)
                                                        {
                                                            id = a.aluno_id;
                                                            status = "OK";
                                                            //MessageBox.Show(i.aluno_id + "\n" + "NÃO TA VENCIDO! 1");
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (DateTime.Today.Month == Convert.ToDateTime(p.pagamento_vencimento).Month)
                                                        {
                                                            if (DateTime.Today.Day < Convert.ToInt32(a.aluno_diaVencimento))
                                                            {
                                                                id = a.aluno_id;
                                                                status = "OK";
                                                                //MessageBox.Show("NÃO TA VENCIDO! 2");
                                                            }
                                                            else
                                                            {
                                                                id = a.aluno_id;
                                                                status = "DEV";
                                                                //MessageBox.Show("1 Vencido a " + resultado.TotalDays.ToString() + " dias!");
                                                            }

                                                        }
                                                        else
                                                        {
                                                            if (DateTime.Today.Month == Convert.ToDateTime(p.pagamento_data).Month)
                                                            {
                                                                if (DateTime.Today.Day < Convert.ToInt32(a.aluno_diaVencimento))
                                                                {
                                                                    id = a.aluno_id;
                                                                    status = "OK";
                                                                    //MessageBox.Show("NÃO TA VENCIDO! 3");
                                                                }
                                                                else
                                                                {
                                                                    id = a.aluno_id;
                                                                    status = "DEV";
                                                                    //MessageBox.Show("2 Vencido a " + resultado.TotalDays.ToString() + " dias!");
                                                                }
                                                            }
                                                            else
                                                            {
                                                                if (resultado.TotalDays < 30)
                                                                {
                                                                    id = a.aluno_id;
                                                                    status = "OK";
                                                                    //MessageBox.Show("3 Vencido a " + resultado.TotalDays.ToString() + " dias!");
                                                                }
                                                                else
                                                                {
                                                                    id = a.aluno_id;
                                                                    status = "DEV";
                                                                    //MessageBox.Show("3 Vencido a " + resultado.TotalDays.ToString() + " dias!");
                                                                }
                                                                //MessageBox.Show(resultado.TotalDays.ToString());
                                                                
                                                            }                                                        
                                                        }                                                      
                                                    }                     
                                                }
                                            }
                                                                                        
                                        }                                        
                                    }catch{}                                    
                                }
                            }    
                    }catch{}
                }
                else
                    {
                        //MessageBox.Show("JÁ ESTA DEVENDO:  ID: " + a.aluno_id);
                        jaDeve = false;
                    }

                 //GRAVAR SITUAÇÃO DO ALUNO BANCO DE DADOS
                if (status != null && status != "")
                {
                    var original = context.gerjfd_aluno.Find(a.aluno_id);
                    original.aluno_status = status;
                    if (original != null)
                    {
                        context.Entry(original).CurrentValues.SetValues(original);
                    }
                    context.SaveChanges();
                    status = "";
                }
                else
                    status = "";
            
            }

            DataGridAlunos.ItemsSource = data.ToList();
        }

        internal void mostrarPagamentos(System.Windows.Controls.DataGrid DataGridPagamentos, int alunoid)
        {
            gerjfdEntities context = new gerjfdEntities();
            var data = (from p in context.gerjfd_pagamento
                        where p.pagamento_aluno_id == alunoid
                        orderby p.pagamento_vencimento
                        select new { p.pagamento_id, p.pagamento_vencimento, p.pagamento_valor, p.pagamento_data, p.pagamento_valorpgt, p.pagamento_status });

            try
            {
                DataGridPagamentos.ItemsSource = data.ToList();
            }
            catch { }
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
            if(Tipo == "Nome"){
                try
                {
                    DataGridAlunos.ItemsSource = data1.ToList();
                }
                catch { }
            }
            else 
                if(Tipo == "Código"){
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
