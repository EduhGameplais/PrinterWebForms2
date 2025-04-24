using Mighty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Printer_Web.WebHoster
{
    internal class ParameterParser
    {
        string Parameters = String.Empty;
        public ParameterParser(HttpListenerRequest request) 
        {
            try
            {
                Parameters = request.RawUrl.Split("?")[1];
            }
            catch 
            {
                Parameters = String.Empty;
                //Log.PrintLn("Error1");
            }
        }

        public string GetParameterValue(string parameter)
        {
            string[] paramsSplited = Parameters.Split('&', '=');
            for (int i = 0; i < paramsSplited.Length; i++)
            {
                
                try
                {
                    if (paramsSplited[i] == parameter)
                    {
                        return paramsSplited[i + 1];
                    }
                }
                catch
                {

                }
            }
            //Log.PrintLn("Error2");
            return String.Empty;
        }
    }
}
