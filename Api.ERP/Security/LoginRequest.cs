using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Api.ERP.Security
{
    public class LoginRequest
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}