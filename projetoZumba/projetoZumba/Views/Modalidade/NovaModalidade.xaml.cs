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

namespace projetoZumba.Views.Modalidade
{
    /// <summary>
    /// Interaction logic for NovaModalidade.xaml
    /// </summary>
    public partial class NovaModalidade : Window
    {
        private Modalidades modalidades;

        public NovaModalidade(Modalidades pModalidades)
        {
            InitializeComponent();
            modalidades = pModalidades;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            gerjfdEntities context = new gerjfdEntities();

            gerjfd_modalidade data = new gerjfd_modalidade()
            {
                modalidade_nome = nomeModalidade.Text,
                modalidade_vlrp = float.Parse(vlrPrincipal.Text),
                modalidade_vlra = float.Parse(vlrAdicional.Text),                
            };
            context.gerjfd_modalidade.Add(data);
            context.SaveChanges();
            modalidades.updateModalidades();
            this.Close();
        }
    }
}
