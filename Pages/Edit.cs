using Printer_Web.WebHoster;
using Printer_Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WebHost;
using Microsoft.VisualBasic.Logging;
using Mighty;

namespace Printer_Web_Forms.Pages
{
    public class Edit
    {
        [Endpoint("/edit")]
        public byte[] ProcessRequest(HttpListenerRequest request)
        {
            ParameterParser parser = new ParameterParser(request);

            

            string clienteName = parser.GetParameterValue("cliente").Replace("%20", " ");
            string boxsizeName = parser.GetParameterValue("boxsizeName");
            string tipo = parser.GetParameterValue("tipo");
            double offset = 0;
            short imagesPerPrint = 0;
            short copies = 0;

            Console.WriteLine(clienteName);

            try
            {
                offset = double.Parse(parser.GetParameterValue("offset"));
                imagesPerPrint = short.Parse(parser.GetParameterValue("imagesPerPrint"));
                copies = short.Parse(parser.GetParameterValue("copies"));
            }
            catch { }
            
            
            string ImagePath = PrintSystem.PATH_TO_CLIENTES_FILES + $"\\{clienteName}-{boxsizeName}-{tipo}.png";
            /*if (!File.Exists(ImagePath))
            {
                //MessageBox.Show(ImagePath);
                ImagePath = "";
                //MessageBox.Show("Não foi possível encontrar o PNG desse cliente com essas configurações de tamanho e tipo.\nVerifique se você configurou corretamente!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }*/

            Mighty.Log.PrintLn(ImagePath);

            PrintSystem.EditPaperSettings(
                    byte.Parse(parser.GetParameterValue("tower")),
                    byte.Parse(parser.GetParameterValue("printer")),
                    ImagePath,
                    boxsizeName,
                    offset,
                    imagesPerPrint,
                    copies
                );

            if (imagesPerPrint == 0 && copies == 0)
            {
                return Encoding.UTF8.GetBytes("edit-offset-success");
            }

            var tower = byte.Parse(parser.GetParameterValue("tower"));
            var printer = byte.Parse(parser.GetParameterValue("printer"));

            return Encoding.UTF8.GetBytes("{\n" +
               $"    \"result\": \"Edited with success. ({tower}/{printer})\"" +
                "\n}");
        }
    }
}
