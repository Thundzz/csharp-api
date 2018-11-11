using System.Web.Http;
using Microsoft.Owin.Hosting;
using System.Web.Http.Cors;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Owin;
using System.Threading;
using System;

namespace SimpleApi
{
    class Program
    {
        static void Main(string[] args)
        {
            WebApp.Start<Startup>("http://localhost:9001/Hello/");
            Console.ReadLine();
        }
    }

    [RoutePrefix("api")]
    public class MyController : ApiController
    {
        [HttpGet]
        [Route("ping")]
        public string Ping()
        {
            return "pong";
        }

        [HttpGet]
        [Route("time")]
        public string Pong()
        {
            return DateTime.Now.ToString();
        }

    }

    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            var configuration = new HttpConfiguration();

            configuration.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            configuration.Formatters.JsonFormatter.SerializerSettings.Formatting = Formatting.Indented;
            configuration.MapHttpAttributeRoutes();
            configuration.EnableCors(new EnableCorsAttribute("*", "*", "*"));
            
            appBuilder.UseWebApi(configuration);
        }
    }

}
