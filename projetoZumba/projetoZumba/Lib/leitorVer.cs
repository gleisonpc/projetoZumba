using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace projetoZumba.Lib
{
    
        class leitorVer: DPFP.Capture.EventHandler
        {
            //VARIAVEIS PARA MONTAR TEMPLATE DE CAPTURA
            DPFP.Capture.Capture Capturer;
            DPFP.Processing.Enrollment Enroller = new DPFP.Processing.Enrollment();
            private DPFP.Verification.Verification Verificator;
            DPFP.Template v = new DPFP.Template();

            public List<byte[]> digData = new List<byte[]>();

            int j = 0;

            public bool status = true;
            public bool verif = false;
            private Views.findAluno find;
            
             public leitorVer(Views.findAluno find)
             {
                 // TODO: Complete member initialization
                 this.find = find;
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
                        SetStatus("USANDO LEITOR DE DIGITAL!!!");
                        Capturer.StartCapture();
                    }
                    catch
                    {
                        SetStatus("NÃO PODE INICIALIZAR A CAPTURA!!!");
                    }
                }
            }

            public void Stop()
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

                    gerjfdEntities context = new gerjfdEntities();

                    foreach (gerjfd_aluno aluno in context.gerjfd_aluno)
                    {

                        try
                        {

                            byte[] bytes = new byte[aluno.aluno_digital1.Length * sizeof(char)];
                            System.Buffer.BlockCopy(aluno.aluno_digital1.ToArray(), 0, bytes, 0, bytes.Length);

                            v.DeSerialize(bytes);

                            Verificator.Verify(features, v, ref result);

                            if (result.Verified)
                            {
                                SetStatus("DEU CERTO A VERIFICAÇÃO.");

                                //MessageBox.Show(aluno.aluno_nome, aluno.aluno_digital1);

                                find.Dispatcher.Invoke(new Action(delegate()
                                {
                                    find.nomeVerif.Content = aluno.aluno_nome;
                                    find.varVerif.Content = result.FARAchieved;
                                    find.verStatus.Content = result.Verified;
                                }));

                                Stop();
                                break;
                            }
                            else
                            {
                                    byte[] bytes2 = new byte[aluno.aluno_digital2.Length * sizeof(char)];
                                    System.Buffer.BlockCopy(aluno.aluno_digital2.ToArray(), 0, bytes2, 0, bytes2.Length);

                                    v = new DPFP.Template();

                                    v.DeSerialize(bytes2);

                                    Verificator.Verify(features, v, ref result);

                                    if (result.Verified)
                                    {
                                        SetStatus("DEU CERTO A VERIFICAÇÃO.");

                                        find.Dispatcher.Invoke(new Action(delegate()
                                        {
                                            find.nomeVerif.Content = aluno.aluno_nome;
                                            find.varVerif.Content = result.FARAchieved;
                                            find.verStatus.Content = result.Verified;
                                        }));

                                        Stop();
                                        break;
                                    }
                                    else
                                    {
                                        SetStatus("VERIFICAÇÃO FALHOU");

                                        find.Dispatcher.Invoke(new Action(delegate()
                                        {
                                            find.nomeVerif.Content = "ALUNO NÃO ENCONTRADO";
                                            find.varVerif.Content = "9999999999";
                                            find.verStatus.Content = result.Verified;
                                        }));

                                        Stop();

                                    }
                                
                            }
                                
                        }
                        catch (Exception err) { MessageBox.Show(err.ToString()); }
                        
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

                if (feedback == DPFP.Capture.CaptureFeedback.Good)
                {

                    return features;

                }
                else

                    return null;
            }

            protected void SetStatus(string status)
            {
                find.Dispatcher.Invoke(new Action(delegate()
                {
                    find.statusLeitura.Content = status;

                }));
            }

            protected void SetDigital(string digital)
            {
                //Digital.Dispatcher.Invoke(new Action(delegate()
               // {
              //      Digital.Text = digital;
              //      dig.Content = digital.GetHashCode().ToString();
               //     numDig.Content = Enroller.FeaturesNeeded.ToString();
              //  }));
            }

            public void OnComplete(object Capture, string ReaderSerialNumber, DPFP.Sample Sample)
            {
                SetStatus("ACABEI DE LER UMA DIGITAL");
                //if (verif)
                //{
                Verify(Sample);
                //}else
                //Process(Sample);

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