using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogLullaby.WEB_API.Infrastructure
{
    public class JWTConfig
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string SecurityKey { get; set; }
        public int LifeTime { get; set; }
        public SymmetricSecurityKey SymmetricSecurityKey
        {
            get
            {
                return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecurityKey));
            }
        }
            
    }
}
