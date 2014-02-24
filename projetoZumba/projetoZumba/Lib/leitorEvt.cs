using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace projetoZumba.Lib
{
    class leitorEvt : DPFP.Capture.EventHandler
    {
        private System.Windows.Controls.Label label;

        public leitorEvt(System.Windows.Controls.Label label)
        {
            // TODO: Complete member initialization
            this.label = label;
        }

        public void OnComplete(object Capture, string ReaderSerialNumber, DPFP.Sample Sample)
        {
            try { label.Content = "ACABEI DE LER UMA DIGITAL"; }
            catch (Exception err) { MessageBox.Show(err.ToString()); }
           
            //if (verif)
            //{
            //    Verify(Sample);
            //}else
            //Process(Sample);

        }

        public void OnFingerGone(object Capture, string ReaderSerialNumber)
        {
            MessageBox.Show("JÁ LI A DIGITAL");
        }

        public void OnFingerTouch(object Capture, string ReaderSerialNumber)
        {
             MessageBox.Show("ESTOU LENDO UMA DIGITAL");
        }

        public void OnReaderConnect(object Capture, string ReaderSerialNumber)
        {
            MessageBox.Show("O LEITOR ESTÁ CONECTADO");
        }

        public void OnReaderDisconnect(object Capture, string ReaderSerialNumber)
        {
            MessageBox.Show("O LEITOR ESTÁ DESCONECTADO!");
        }

        public void OnSampleQuality(object Capture, string ReaderSerialNumber, DPFP.Capture.CaptureFeedback CaptureFeedback)
        {
            if (CaptureFeedback == DPFP.Capture.CaptureFeedback.Good)
                MessageBox.Show("A DIGITAL É MUITO BOA");
            else
                MessageBox.Show("PASSA DE NOVO QUE DEU MERDA!!!!");
        }
    }
}
