using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.API.Model
{
    public class TokenViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Repassword { get; set; }
        public string ApiToken { get; set; }
    }
}
