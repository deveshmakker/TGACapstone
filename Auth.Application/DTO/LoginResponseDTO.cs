using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.DTO
{
    public class LoginResponseDTO
    {
        public UserDTO User { get; set; }
        public TokenRequestDTO Token { get; set; }
    }
}
