using System.Web.Http;
using Newtonsoft.Json.Serialization;
using Owin;
using Swashbuckle.Application;
using System;
using System.Reflection;
using System.Linq;
using Microsoft.Owin.Cors;
using Api.ERP.Swagger;

namespace Api.ERP.Start
{
    public class ApiConfig
    {
        public static void Configuration(IAppBuilder app)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            AssemblyName assemblyName = assembly.GetName();
            var config = new HttpConfiguration();

            // Configuração das Rotas por Atributos
            config.MapHttpAttributeRoutes();

            // Configuração de Rotas Padrão do WebApi 
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Removendo o formatter de xml para melhorar o uso pelo browser 
            // -- comentar/remover essa linha se quiser suportar XML
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new DefaultContractResolver();

            // Habilitanto CORS
            app.UseCors(CorsOptions.AllowAll);

            // Habilitando Documentação
            config.EnableSwagger((c) =>
            {
                var x = new Uri(Assembly.GetExecutingAssembly().CodeBase);
                var commentsFile = x.LocalPath.ToLower().Replace(".dll", ".xml");

                c.SingleApiVersion("v" + assemblyName.Version.ToString().Replace(".", "0"), "IntegracaoParceiros");
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                c.IncludeXmlComments(commentsFile);
                c.OperationFilter(() => new AddTokenHeaderParameter());

                c.RootUrl(req => new Uri(req.RequestUri, System.Web.HttpContext.Current.Request.ApplicationPath ?? string.Empty).ToString());
            })
            .EnableSwaggerUi();

            // Configurando o WebApi
            app.UseWebApi(config);
        }
    }
}