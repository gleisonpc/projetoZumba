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
using System.Threading;

namespace projetoZumba
{
    /// <summary>
    /// Interaction logic for leitorDig.xaml
    /// </summary>
    public partial class leitorDig : Window
    {
        public bool status;
        private leitorLib leitor;

        private TextBox Digital1;

        public leitorDig(TextBox Digital1)
        {
            InitializeComponent();

            leitor = new leitorLib(this, Digital1);

            this.Digital1 = Digital1;

            leitor.Init();
            leitor.Start();
            
        }

        private void numLeit_LayoutUpdated(object sender, EventArgs e)
        {
            
            if(numLeit.Content.ToString() == "0"){

                okbutton.IsEnabled = true;

            }
            
        }

        private void okbutton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            leitor.Stop();
            this.Close();
        }

                  

    }
}