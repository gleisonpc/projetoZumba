using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Printing;
using System.Web;

namespace TesteImpressora
{
    class Impressora
    {
        private static string font  = "Arial";
        private static int fontSize = 15;
        private static FontStyle fontStyle = FontStyle.Bold;
        private static Color fontColor = Color.Black;


        public static void imprimir(string msg)
        {
            PrinterSettings settings = new PrinterSettings();
            PrintDocument p = new PrintDocument();
            p.PrintPage += delegate(object sender1, PrintPageEventArgs e1)
            {
                e1.Graphics.DrawString(msg, new Font(font, fontSize, fontStyle), new SolidBrush(fontColor), new RectangleF(0, 0, p.DefaultPageSettings.PrintableArea.Width, p.DefaultPageSettings.PrintableArea.Height));

            };
            p.PrinterSettings.PrinterName = settings.PrinterName;
            p.Print();
        }

        public static void setFont(string pFont)
        {
            font = pFont;
        }

        public static void setFontSize(int pFontSize)
        {
            fontSize = pFontSize;
        }

        public static void setFontStyle(FontStyle pFontStyle)
        {
            fontStyle = pFontStyle;
        }

        public static void setFontColor(Color pFontColor)
        {
            fontColor = pFontColor;
        }
    }
}
