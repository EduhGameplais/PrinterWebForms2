using Printer_Web.WebHoster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WebHost;
using Mighty;
using System.Drawing.Printing;

namespace Printer_Web.Pages
{
    public class Home : IEndpoint
    {
        public void EndpointInit() { }

        [Endpoint("/")]
        public byte[] ProcessGotoRequest(HttpListenerRequest request)
        {
            PrintSystem.ScanForClientes();
            return Encoding.UTF8.GetBytes(File.ReadAllText("Pages/goto.html"));
        }

        [Endpoint("/tower1")]
        public byte[] ProcessRequest(HttpListenerRequest request)
        {
            PrintSystem.ScanForClientes();
            return Encoding.UTF8.GetBytes(File.ReadAllText("Pages/home.html").Replace("tower1", "tower1").Replace("Torre 1", "Torre 1"));
        }
        [Endpoint("/tower2")]
        public byte[] Process2Request(HttpListenerRequest request)
        {
            PrintSystem.ScanForClientes();
            return Encoding.UTF8.GetBytes(File.ReadAllText("Pages/home.html").Replace("tower1", "tower2").Replace("Torre 1", "Torre 2"));
        }
        [Endpoint("/tower3")]
        public byte[] Process3Request(HttpListenerRequest request)
        {
            PrintSystem.ScanForClientes();
            return Encoding.UTF8.GetBytes(File.ReadAllText("Pages/home.html").Replace("tower1", "tower3").Replace("Torre 1", "Torre 3"));
        }

        [Endpoint("/style.css")]
        public byte[] ReturnCSS(HttpListenerRequest request)
        {
            return File.ReadAllBytes("Pages/style.css");
        }
        [Endpoint("/update.js")]
        public byte[] ReturnUpdateJS(HttpListenerRequest request)
        {
            return File.ReadAllBytes("Pages/update.js");
        }

        [Endpoint("/tower1.js")]
        public byte[] ReturnTower1(HttpListenerRequest request)
        {
            return File.ReadAllBytes("Pages/tower1.js");
        }
        [Endpoint("/tower2.js")]
        public byte[] ReturnTower2(HttpListenerRequest request)
        {
            return File.ReadAllBytes("Pages/tower2.js");
        }
        [Endpoint("/tower3.js")]
        public byte[] ReturnTower3(HttpListenerRequest request)
        {
            return File.ReadAllBytes("Pages/tower3.js");
        }



        [Endpoint("/clientes")]
        public byte[] GetClientes(HttpListenerRequest request)
        {
            // Retornar os dados dos clientes
            string clientesVirgula = "";

            foreach(var cliente in PrintSystem.Clientes)
            {
                clientesVirgula += $"\"{cliente}\",";
            }

            clientesVirgula = clientesVirgula.Remove(clientesVirgula.Length - 1, 1);

            string clientesJson = "{\n" +
                $"    \"clientes\": [{clientesVirgula}]\n" + 
                "}";
            return Encoding.UTF8.GetBytes(clientesJson);
        }

        [Endpoint("/tamanhos")]
        public byte[] GetSizes(HttpListenerRequest request)
        {
            string capsuledContent = String.Empty;
            for(int i = 0; i < PrintSystem.Tamanhos.Count; i++)
            {
                var tempSize = PrintSystem.Tamanhos[i];
                capsuledContent += $"        [\"{tempSize.Name}\", {tempSize.Largura}, {tempSize.Comprimento}]";

                if (i != PrintSystem.Tamanhos.Count-1)
                {
                    capsuledContent += ",\n";
                }
            }

            // Retornar os dados dos clientes
            string tamanhosJson = "{\n" +
            "    \"tamanhos\": [\n" +
            $"{capsuledContent}\n" +
            "    ]\n" +
            "}";
            return Encoding.UTF8.GetBytes(tamanhosJson);
        }

        [Endpoint("/tipos")]
        public byte[] GetTypes(HttpListenerRequest request)
        {
            string tiposVirgula = "";

            try
            {
                foreach (var tipo in PrintSystem.Tipos)
                {
                    tiposVirgula += $"\"{tipo.Trim()}\",";
                }

                tiposVirgula = tiposVirgula.Remove(tiposVirgula.Length - 1, 1);
            } catch { }

            if (tiposVirgula == "\"\"")
                tiposVirgula = "";

            // Retornar os dados dos clientes
            string tiposJson = "{\n" +
                $"    \"tipos\": [{tiposVirgula}]\n" +
                "}";
            return Encoding.UTF8.GetBytes(tiposJson);
        }

        [Endpoint("/offset")]
        public byte[] GetOffset(HttpListenerRequest request)
        {
            ParameterParser parser = new ParameterParser(request);

            var tower_id = byte.Parse(parser.GetParameterValue("tower"));

            double offset1 = PrintSystem.GetOffset(tower_id, 1);
            double offset2 = PrintSystem.GetOffset(tower_id, 2);
            double offset3 = PrintSystem.GetOffset(tower_id, 3);

            string tiposJson = "{\n" +
                $"    \"offset1\": \"{offset1}\",\n" +
                $"    \"offset2\": \"{offset2}\",\n" +
                $"    \"offset3\": \"{offset3}\"\n" +
                "}";

            return Encoding.UTF8.GetBytes(tiposJson);
        }
    }
}
