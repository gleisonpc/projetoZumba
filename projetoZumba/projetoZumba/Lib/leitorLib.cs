using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows;

namespace projetoZumba.Lib
{
    class leitorLib : DPFP.Capture.EventHandler
    {
        DPFP.Capture.Capture Capturer;
        DPFP.Processing.Enrollment Enroller = new DPFP.Processing.Enrollment();
        private DPFP.Verification.Verification Verificator;
        DPFP.Template v = new DPFP.Template();
                
        public List <byte[]> digData = new List<byte[]>();

        int j = 0;

        public bool status = true;
        public bool verif = false;
        private System.Windows.Controls.Label infoLabel;
        private System.Windows.Controls.TextBox Digital;

        public delegate void teste();

        public leitorLib(System.Windows.Controls.Label infoLabel, System.Windows.Controls.TextBox Digital)
        {
            // TODO: Complete member initialization
            this.infoLabel = infoLabel;
            this.Digital = Digital;
        }


        public void Init()
        {
            try
            {
                Capturer = new DPFP.Capture.Capture();				// Create a capture operation.

                if (null != Capturer)
                {
                    Capturer.EventHandler = this;					// Subscribe for capturing events.
                }
                else
                    SetStatus("NÃO PODE INICIAR A CAPTURA");
            }
            catch
            {
                SetStatus("NÃO PODE INICIAR A CAPTURA");
            }
        }

        public void Start()
        {
            if (null != Capturer)
            {
                try
                {
                    infoLabel.Content = "USANDO LEITOR DE DIGITAL!!!";
                    Capturer.StartCapture();
                }
                catch
                {
                    SetStatus("NÃO PODE INICIALIZAR A CAPTURA!!!");
                }
            }
        }

        protected void Stop()
        {
            if (null != Capturer)
            {
                try
                {
                    Capturer.StopCapture();
                }
                catch
                {
                    SetStatus("NAO PODE TERMINAR A CAPTURA");
                }
            }
        }

        public void Process(DPFP.Sample Sample)
        {
            
            string str;
            DPFP.FeatureSet features = ExtractFeatures(Sample, DPFP.Processing.DataPurpose.Enrollment);

			// Check quality of the sample and add to enroller if it's good
			if (features != null) try
			{
               Enroller.AddFeatures(features);		// Add feature set to template.
            }
            catch {

                infoLabel.Content = "DIGITAL SEM QUALIDADE!!!";
            
            }
 
            switch (Enroller.TemplateStatus)
            {
                case DPFP.Processing.Enrollment.Status.Ready:

                    byte[] t = new byte[Enroller.Template.Size];

                    Enroller.Template.Serialize(ref t);

                    digData.Add(t);

                    System.Text.ASCIIEncoding serial = new System.Text.ASCIIEncoding();

                    str = serial.GetString(t);

                    SetStatus(str);
                    SetDigital(str);

                    Enroller.Clear();
                    Stop();
                    status = false;
                    

                break;
            }
           
        }

        public void Verify(DPFP.Sample Sample)
        {
            Verificator = new DPFP.Verification.Verification();

            // Process the sample and create a feature set for the enrollment purpose.
            DPFP.FeatureSet features = ExtractFeatures(Sample, DPFP.Processing.DataPurpose.Verification);

            // Check quality of the sample and start verification if it's good
            // TODO: move to a separate task

            
            if (features != null)
            {
                // Compare the feature set with our template
                DPFP.Verification.Verification.Result result = new DPFP.Verification.Verification.Result();

                foreach (byte[] i in digData)
                {
                    try
                    {
                        v.DeSerialize(i);

                        Verificator.Verify(features, v, ref result);

                        Console.WriteLine(result.FARAchieved);
                        Console.WriteLine(result.Verified);

                        if (result.Verified)
                            SetStatus("DEU CERTO A VERIFICAÇÃO.");
                        else
                            SetStatus("DEU RUIM!");
                    }
                    catch (Exception err) { Console.WriteLine(err); }

                    
                   
                }
            }
        }

        protected Bitmap ConvertSampleToBitmap(DPFP.Sample Sample)
        {
            DPFP.Capture.SampleConversion Convertor = new DPFP.Capture.SampleConversion();	// Create a sample convertor.
            Bitmap bitmap = null;												            // TODO: the size doesn't matter
            Convertor.ConvertToPicture(Sample, ref bitmap);									// TODO: return bitmap as a result
            return bitmap;
        }

        protected DPFP.FeatureSet ExtractFeatures(DPFP.Sample Sample, DPFP.Processing.DataPurpose Purpose)
        {
            DPFP.Processing.FeatureExtraction Extractor = new DPFP.Processing.FeatureExtraction();	// Create a feature extractor
            
            DPFP.Capture.CaptureFeedback feedback = DPFP.Capture.CaptureFeedback.None;
            
            DPFP.FeatureSet features = new DPFP.FeatureSet();
            
            Extractor.CreateFeatureSet(Sample, Purpose, ref feedback, ref features);			// TODO: return features as a result?
            
            if (feedback == DPFP.Capture.CaptureFeedback.Good){

                return features;

            }else

            return null;
        }

        protected void SetStatus(string status)
        {
            infoLabel.Dispatcher.Invoke(new Action(delegate()
            {
                infoLabel.Content = status;
            }));
        }

        protected void SetDigital(string digital)
        {
            Digital.Dispatcher.Invoke(new Action(delegate()
            {
                Digital.Text = digital;
            }));
        }

        public void OnComplete(object Capture, string ReaderSerialNumber, DPFP.Sample Sample)
        {
            SetStatus("ACABEI DE LER UMA DIGITAL");
            //if (verif)
            //{
            //    Verify(Sample);
            //}else
            Process(Sample);
            
        }

        public void OnFingerGone(object Capture, string ReaderSerialNumber)
        {
            SetStatus("JÁ LI A DIGITAL");
        }

        public void OnFingerTouch(object Capture, string ReaderSerialNumber)
        {
            SetStatus("ESTOU LENDO UMA DIGITAL");
           
        }

        public void OnReaderConnect(object Capture, string ReaderSerialNumber)
        {
            SetStatus("O LEITOR ESTÁ CONECTADO");
        }

        public void OnReaderDisconnect(object Capture, string ReaderSerialNumber)
        {
            SetStatus("O LEITOR ESTÁ DESCONECTADO!");
        }

        public void OnSampleQuality(object Capture, string ReaderSerialNumber, DPFP.Capture.CaptureFeedback CaptureFeedback)
        {
            if (CaptureFeedback == DPFP.Capture.CaptureFeedback.Good)
                SetStatus("A DIGITAL É MUITO BOA");
            else
                SetStatus("PASSA DE NOVO QUE DEU MERDA!!!!");
        }
    }
}