using Microsoft.AspNetCore.Identity;

namespace ProjectX.Data.Entities
{

    public class AppUser:IdentityUser
    {
        public int Age { get; set; }
        public string FullName { get; set; }
    }
}
