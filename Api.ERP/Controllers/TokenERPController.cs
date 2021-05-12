using Api.ERP.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Api.ERP.Controllers
{
    public class TokenERPController : ApiController
    {

        /// <summary>
        /// Efetua a Autenticação do Usuário
        /// </summary>
        /// <param name="login">Login do Usuário para a Autenticação, composto de login e senha. Esse login é fornecido diretamente ao parceiro</param>
        /// <returns>Token de Autenticação a ser utilizado nas requisições privadas</returns>
        [Route("api/token/post")]
        public IHttpActionResult Post([FromBody] LoginRequest login)
        {
            try
            {
                //Senha genérica, se houver controle de login no cliente, precisa fazer consumo ou leitura do banco para obter login e senha válidos
                if (string.Compare(login.Login, "login", StringComparison.CurrentCultureIgnoreCase) == 0 && string.Compare(login.Password, "S3nh@", StringComparison.CurrentCultureIgnoreCase) == 0)
                {
                    var token = new TokenERP
                    {
                        UsuarioId = 0,
                        Login = login.Login,
                        DataExpiracao = DateTime.Today.AddDays(1).AddMinutes(-1)
                    };

                    if (token != null)
                        return Ok(token.GerarTokenString());
                }

                return Unauthorized();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}