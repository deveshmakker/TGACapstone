using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.DTO
{
    public class TokenRequestDTO
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
