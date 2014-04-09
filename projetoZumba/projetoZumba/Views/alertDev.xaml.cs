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

using Microsoft.Reporting.WebForms;
using System.IO;
using System.Diagnostics;

namespace projetoZumba.Views
{
    /// <summary>
    /// Interaction logic for alertDev.xaml
    /// </summary>
    public partial class alertDev : Window
    {
        public alertDev()
        {
            InitializeComponent();

            gerjfdEntities context = new gerjfdEntities();

            var data = context.gerjfd_aluno.Where(x => x.aluno_status == "DEV").GroupBy(x => x.aluno_id).Count();

            textoDev.Text = "Existem " + data.ToString() + " devedores no Sistema!";

            if (Convert.ToInt32(data.ToString()) > 0)
            {
                this.Show();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            gerjfdEntities context = new gerjfdEntities();

            ReportViewer reportviewer = new ReportViewer();

            reportviewer.ProcessingMode = ProcessingMode.Local;

            reportviewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", context.gerjfd_view_dev));

            reportviewer.LocalReport.ReportEmbeddedResource = "projetoZumba.Report.AlunoDev.rdlc";

            Warning[] warnings;
            String[] streamids;
            string mimeType;
            string encoding;
            string extension;
            
            try
            {
                byte[] bytePDF = reportviewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamids, out warnings);
                FileStream fileStreamPDF = null;
                string nomeArquivoPDF = System.IO.Path.GetTempPath() + "RelDev" + DateTime.Now.ToString("dd_MM_yyyy-HH_mm_ss") + ".pdf";

                fileStreamPDF = new FileStream(nomeArquivoPDF, FileMode.Create);
                fileStreamPDF.Write(bytePDF, 0, bytePDF.Length);
                fileStreamPDF.Close();

                Process.Start(nomeArquivoPDF);
            }
            catch (Exception Err) { MessageBox.Show(Err.ToString()); }
            
            //FileStream fileStreamPDF = null;
            //string nomeArquivoPDF = System.IO.Path.GetTempPath() + "RelDev" + DateTime.Now.ToString("dd_MM_yyyy-HH_mm_ss") + ".pdf";
            
            //fileStreamPDF = new FileStream(nomeArquivoPDF, FileMode.Create);
            //fileStreamPDF.Write(bytePDF, 0, bytePDF.Length);
            //fileStreamPDF.Close();

            //Process.Start(nomeArquivoPDF);
        
        
        }

        
    }
}
