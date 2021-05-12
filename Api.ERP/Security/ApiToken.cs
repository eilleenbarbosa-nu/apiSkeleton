using JWT;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;

namespace Api.ERP.Security
{
    public static class ApiToken
    {
        public static string Secret { get; set; }

        public static void GerarSecret()
        {
            var secret = Guid.NewGuid().ToString("N").Substring(0, 20);
            Secret = secret;
        }

        public static string GerarTokenString(this TokenERP token)
        {
            if (string.IsNullOrWhiteSpace(Secret))
                throw new Exception("A chave de criptografia está ausente!");

            return JsonWebToken.Encode(token, Secret, JwtHashAlgorithm.HS256);
        }

        public static TokenERP RecuperarToken(this HttpActionContext actionContext)
        {
            string json;
            try
            {
                var tokenString = actionContext.Request.Headers.GetValues("Token").FirstOrDefault();
                json = JsonWebToken.Decode(tokenString, Secret);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }

            return JsonConvert.DeserializeObject<TokenERP>(json);
        }
    }    
}