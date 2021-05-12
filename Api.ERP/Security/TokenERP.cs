using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.ERP.Security
{
    public class TokenERP
    {
        public long UsuarioId { get; set; }
        public string Login { get; set; }
        public DateTime DataExpiracao { get; set; }
    }
}