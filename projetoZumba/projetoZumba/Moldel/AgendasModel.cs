
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace projetoZumba.Moldel
{
    class AgendasModel
    {
        internal void mostrarAgendas(System.Windows.Controls.DataGrid DataGridAgendas)
        {
            gerjfdEntities context = new gerjfdEntities();
            var data = context.gerjfd_agenda.GroupJoin(context.gerjfd_modalidade, x => x.agenda_id_modalidade, y => y.modalidade_id, (x, y) => new { x, y }).Where(x => x.x.agenda_ativa == "Y").SelectMany(temp => temp.y.DefaultIfEmpty(), (x, y) => new { x.x.agenda_id, x.x.agenda_dia_semana, x.x.agenda_horario, y.modalidade_nome }).OrderBy(x => new { x.agenda_dia_semana, x.agenda_horario });
            DataGridAgendas.ItemsSource = data.ToList();
        }

        
    }
}
