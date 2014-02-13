using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projetoZumba
{
    class AlunosModel
    {
        internal void mostrarAlunos(System.Windows.Controls.DataGrid DataGridAlunos)
        {
            gerjfdEntities context = new gerjfdEntities();
            DataGridAlunos.ItemsSource = context.gerjfd_aluno.ToList();
        }
    }
}
