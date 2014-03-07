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

namespace projetoZumba.Views
{

    
    /// <summary>
    /// Interaction logic for findAluno.xaml
    /// </summary>
    public partial class findAluno : Window
    {

        private leitorVer leitor;
        
        public findAluno()
        {
            InitializeComponent();

            leitor = new leitorVer(this);

            leitor.Init();
            leitor.Start();
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            leitor.Stop();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            leitor.Stop();
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            leitor = new leitorVer(this);

            leitor.Init();
            leitor.Start();
        }

        private void Button_LayoutUpdated(object sender, EventArgs e)
        {
            if (verStatus.Content != "") {

                bottonok.IsEnabled = true;

            }
        }
    
    }
}
