using Microsoft.AspNetCore.Identity;

namespace Auth.Common.Model
{
    public class ApplicationUser : IdentityUser
    {        
        public string Name { get; set; }     
    }
}
