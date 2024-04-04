using Microsoft.AspNetCore.Identity;


namespace Pustok.Core.Entities.Identity
{
    public class AppUser:IdentityUser
    {
        public string Fullname { get; set; }

    }
}
