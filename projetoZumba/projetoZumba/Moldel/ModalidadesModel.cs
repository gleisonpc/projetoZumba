using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projetoZumba.Moldel
{
    class ModalidadesModel
    {
        internal void mostrarModalidades(System.Windows.Controls.DataGrid DataGridModalidades)
        {
            gerjfdEntities context = new gerjfdEntities();
            var data = (from p in context.gerfd_modalidade select new { p.modalidade_nome, p.modalidade_vlrp, p.modalidade_vlra });
            DataGridModalidades.ItemsSource = data.ToList();

        }
    }
}
