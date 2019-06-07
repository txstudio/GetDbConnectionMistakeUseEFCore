using System.Collections.Generic;

namespace ConsoleApp
{
    public class ApplicationUser
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public virtual ICollection<ApplicationUserInRole> UserInRoles { get; set; }
    }

}
