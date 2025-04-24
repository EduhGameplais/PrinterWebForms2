using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WebHost
{
    public interface ILocalFunction
    {
        void Enable();
    }
    public interface IEndpoint
    {
        void EndpointInit();
        byte[] ProcessRequest(HttpListenerRequest request);
    }

    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public sealed class StartMethodAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public class EndpointAttribute : Attribute
    {
        public string Path { get; }

        public EndpointAttribute(string path)
        {
            Path = path;
        }
    }

    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public class EndpointInitAttribute : Attribute
    {
        public string Path { get; }

        public EndpointInitAttribute(string path)
        {
            Path = path;
        }
    }
}
