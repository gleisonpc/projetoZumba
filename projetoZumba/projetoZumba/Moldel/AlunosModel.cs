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
            var data = (from p in context.gerjfd_aluno select new { p.aluno_id, p.aluno_nome});
            DataGridAlunos.ItemsSource = data.ToList();
        }
    }
}
