using Printer_Web.WebHoster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WebHost;

namespace Printer_Web.Pages
{
    public class Print 
    {
        [Endpoint("/print")]
        public byte[] ProcessRequest(HttpListenerRequest request)
        {
            ParameterParser parser = new ParameterParser(request);

            string result = PrintSystem.Print(byte.Parse(parser.GetParameterValue("tower")), byte.Parse(parser.GetParameterValue("printer")));

            if(result == "success")
            {
                return Encoding.UTF8.GetBytes("printing-started" + parser.GetParameterValue("printer"));
            }
            else
            {
                return Encoding.UTF8.GetBytes(result);
            }

            
        }
    }
}
