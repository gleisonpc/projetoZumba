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
        private leitorEvt evt;

        private TextBox Digital1;
        
        public leitorDig()
        {
            InitializeComponent();

            evt = new leitorEvt(infoLabel);
            leitor = new leitorLib(infoLabel, evt);

            leitor.Init();
            leitor.Start();
        } 

    }
}