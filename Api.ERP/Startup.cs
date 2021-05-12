using Api.ERP.Security;
using Api.ERP.Start;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

//Desenvolvido por Eilleen Dalla-Lana Barbosa
//Contato: https://www.linkedin.com/in/eilleen-dalla-lana-barbosa-809a8a80/

namespace Api.ERP
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ApiConfig.Configuration(app);
            ApiToken.GerarSecret();
        }
    }
}