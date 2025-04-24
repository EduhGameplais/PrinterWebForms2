using Mighty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WebHost
{
    public static class WebService
    {
        private static string FOURZEROFOURPage = "<html>\r\n    <body>\r\n        <h1>404</h1>\r\n    </body>\r\n</html>";

        public static void Start()
        {
            Log.PrintLn("Iniciando Endpoints");
            InitializeEndpoints();

            HttpListener listener = new HttpListener();
            listener.Prefixes.Add("http://+:80/"); ;
            listener.Start();

            Log.PrintLn("Porta: 80");

            while (true)
            {
                HttpListenerContext context = listener.GetContext();
                HttpListenerRequest request = context.Request;
                HttpListenerResponse response = context.Response;

                // Obtém todas as classes no assembly atual que têm o atributo EndpointAttribute
                var assembly = Assembly.GetExecutingAssembly();
                var methodsWithEndpointAttribute = assembly.GetTypes()
                    .SelectMany(t => t.GetMethods())
                    .Where(m => Attribute.IsDefined(m, typeof(EndpointAttribute)))
                    .ToList();

                bool EndpointFind = false;

                // Chama o método associado ao atributo em todas as instâncias das classes marcadas
                foreach (var methodInfo in methodsWithEndpointAttribute)
                {
                    var declaringType = methodInfo.DeclaringType;
                    var instance = Activator.CreateInstance(declaringType);

                    // Obtém o atributo EndpointAttribute associado ao método
                    var endpointAttribute = (EndpointAttribute)Attribute.GetCustomAttribute(methodInfo, typeof(EndpointAttribute));

                    string URLPath = request.RawUrl.Split("?")[0];

                    // Obtém o valor do atributo Path
                    if (endpointAttribute != null)
                    {
                        string path = endpointAttribute.Path;
                        if (path == URLPath)
                        {
                            EndpointFind = true;
                            byte[] buffer = new byte[1];
                            try
                            {
                                byte[] buffer2 = (byte[])methodInfo.Invoke(instance, new object[] { request });
                                buffer = new byte[buffer2.Length];
                                buffer = buffer2;
                            }
                            catch(Exception ex)
                            {
                                Log.PrintError(ex);
                            }

                            // Adiciona o cabeçalho de CORS
                            response.AddHeader("Access-Control-Allow-Origin", "*");

                            Log.PrintLn("Request de '" + request.RemoteEndPoint + "' no endpoint '" + URLPath + "'.");
                            response.ContentLength64 = buffer.Length;
                            System.IO.Stream output = response.OutputStream;
                            output.Write(buffer, 0, buffer.Length);
                            output.Close();
                        }
                    }

                }

                if (!EndpointFind)
                {
                    string responseString = FOURZEROFOURPage;
                    byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);

                    Log.PrintLn("Request of '" + request.RemoteEndPoint + "' at '" + request.RawUrl + "' endpoint. $Red$(404)");
                    response.ContentLength64 = buffer.Length;
                    System.IO.Stream output = response.OutputStream;
                    output.Write(buffer, 0, buffer.Length);
                    output.Close();
                }
            }
        }

        public static void SetFourZeroFourPage(string html)
        {
            FOURZEROFOURPage = html;
        }

        private static void InitializeEndpoints()
        {
            // Obtém todas as classes no assembly atual que têm o atributo EndpointAttribute
            var assembly = Assembly.GetExecutingAssembly();
            var methodsWithEndpointAttribute = assembly.GetTypes()
                .SelectMany(t => t.GetMethods())
                .Where(m => Attribute.IsDefined(m, typeof(EndpointInitAttribute)))
                .ToList();

            string Endpoints = "";

            // Chama o método associado ao atributo em todas as instâncias das classes marcadas
            foreach (var methodInfo in methodsWithEndpointAttribute)
            {
                var declaringType = methodInfo.DeclaringType;
                var instance = Activator.CreateInstance(declaringType);

                // Obtém o atributo EndpointAttribute associado ao método
                var endpointAttribute = (EndpointInitAttribute)Attribute.GetCustomAttribute(methodInfo, typeof(EndpointInitAttribute));

                // Obtém o valor do atributo Path
                if (endpointAttribute != null)
                {
                    string path = endpointAttribute.Path;
                    //Console.WriteLine($"Método: {declaringType.Name}.{methodInfo.Name}, Path: {path}");

                    Log.PrintLn($"$Green$Endpoint $White$'{path}' $Green$foi iniciado.");
                }

                // Chama o método
                methodInfo.Invoke(instance, null);
            }
        }
    }
}
