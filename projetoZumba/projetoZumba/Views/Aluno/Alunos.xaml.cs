﻿using System;
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
using projetoZumba.Views;
using projetoZumba.Views.Pagamentos;
using projetoZumba.Moldel;

namespace projetoZumba.Views.Aluno
{
    /// <summary>
    /// Interaction logic for Alunos.xaml
    /// </summary>
    public partial class Alunos : Window
    {
        AlunosModel alunosModel = new AlunosModel();
        PagamentosModel pagamentosModel = new PagamentosModel(); 

        public Alunos()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.updateAlunos();
        }

        private void AlunosNovo_Click(object sender, RoutedEventArgs e)
        {
            NovoAluno alunosDetalhado = new NovoAluno(this);
            alunosDetalhado.Show();
        }

        public void updateAlunos()
        {
            pagamentosModel.mostrarAlunos(DataGridAlunos);
        }

        private void DataGridAlunos_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            dynamic aluno =  DataGridAlunos.SelectedItem;
            if (aluno != null)
            {
                int id = aluno.aluno_id;
                gerjfdEntities context = new gerjfdEntities();

                foreach (gerjfd_aluno alunoBanco in context.gerjfd_aluno)
                {
                    if (alunoBanco.aluno_id == id)
                    {
                        EditarAluno editarAluno = new EditarAluno(alunoBanco, this);
                        editarAluno.Show();
                    }
                }
            }            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            updateAlunos();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            alunosModel.mostrarBuscaAlunos(DataGridAlunos, campoBusca.Text, tipoBusca.Text);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            dynamic aluno = DataGridAlunos.SelectedItem;
            if (aluno != null)
            {
                int id = aluno.aluno_id;
                gerjfdEntities context = new gerjfdEntities();

                foreach (gerjfd_aluno alunoBanco in context.gerjfd_aluno)
                {
                    if (alunoBanco.aluno_id == id)
                    {
                        PagamentoAluno pagamentoAluno = new PagamentoAluno(alunoBanco, this);
                        pagamentoAluno.Show();
                    }
                }
            }            
        }

        private void campoBusca_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                alunosModel.mostrarBuscaAlunos(DataGridAlunos, campoBusca.Text, tipoBusca.Text);
            }
            
        }
    }
}
