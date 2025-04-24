using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Printing;
using System.Drawing;

namespace Printer_Web
{
    public class Printer
    {
        public string Name = String.Empty;

        PaperSettings printingConfig = PaperSettings.Empty();

        static bool cancelPrint = false;

        public Printer(string Name)
        {
            this.Name = Name;
        }

        public void Print(PaperSettings printingConfig)
        {
            this.printingConfig = printingConfig;

            PrintDocument pd1 = new PrintDocument();

            PrinterSettings ps = new PrinterSettings();

            ps.PrintFileName = Name;
            pd1.DocumentName = Name;

            pd1.PrinterSettings = ps;
            pd1.PrinterSettings.PrinterName = Name;
            pd1.PrinterSettings.PrintFileName = Name + "FIle";
            pd1.PrintPage += PrintPage;
            pd1.PrinterSettings.Copies = printingConfig.Copies;

            // Criar um novo processo para imprimir e não travar o programa todo.
            Task.Run(() =>
            {
                pd1.Print();
            });

        }
        private void PrintPage(object o, PrintPageEventArgs e)
        {
            Image img = Image.FromFile(printingConfig.ImagePath);


            for (int i = 0; i < printingConfig.ImagesPerPrint; i++)
            {
                int offsetPrimeiraFolha = 3;
                if (i > 0)
                {
                    offsetPrimeiraFolha = 0;
                }
                var destinationRect = new Rectangle(-12, (int)(((((printingConfig.Tamanho.Comprimento / 2.54) * 100) + (printingConfig.Offset / 25.4) * 100) * i) - ((4 / 25.4) * 100)), (int)((printingConfig.Tamanho.Largura / 2.54) * 100), (int)((printingConfig.Tamanho.Comprimento / 2.54) * 100) + (int)((printingConfig.Offset / 25.4) * 100));

                e.Graphics.DrawImage(img, destinationRect);

                e.Cancel = cancelPrint;
                
            }

        }
        public static void CancelPrint()
        {
            cancelPrint = true;
        }

    }
    public class PaperSettings
    {
        public double Offset;
        public BoxSize Tamanho;
        public short ImagesPerPrint;
        public string ImagePath = String.Empty;
        public short Copies;

        public PaperSettings(string imagePath, BoxSize tamanho, double offset, short imagesPerPrint, short copies)
        {
            Offset = offset;
            Tamanho = tamanho;
            ImagesPerPrint = imagesPerPrint;
            ImagePath = imagePath;
            Copies = copies;
        }

        public static PaperSettings Empty() {
            return new PaperSettings(String.Empty, new BoxSize(), 0, 0, 0);
        }
    }
    public class BoxSize
    {
        public string? Name;
        public double Largura;
        public double Comprimento;

        public BoxSize() { }
        public BoxSize(string name, double largura, double comprimento)
        {
            Name = name;
            Largura = largura;
            Comprimento = comprimento;
        }
        public static BoxSize? FindBoxSize(BoxSize[] boxSizes, string findName)
        {
            try
            {
                foreach (BoxSize box in boxSizes)
                {
                    if (box.Name == findName) return box;
                }
            }catch { }
            return null;
        }
    }
}
