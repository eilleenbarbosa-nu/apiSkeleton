using Api.ERP.Security;
using JWT;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Api.ERP.Filters
{
    public class IntegracaoAutorizationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            TokenERP token = null;
            try
            {
                token = actionContext.RecuperarToken();
            }
            catch (SignatureVerificationException)
            {
                TokenExpirado(actionContext);
            }
            catch (Exception ex)
            {
                actionContext.Response = actionContext.Request.CreateResponse(
                    HttpStatusCode.InternalServerError,
                    ex);
            }

            if (token == null || DateTime.Now > token.DataExpiracao)
                TokenExpirado(actionContext);

            base.OnActionExecuting(actionContext);
        }

        private static void TokenExpirado(HttpActionContext actionContext)
        {
            actionContext.Response = actionContext.Request.CreateResponse(
                HttpStatusCode.Unauthorized,
                "Invalid token");
        }
    }
}