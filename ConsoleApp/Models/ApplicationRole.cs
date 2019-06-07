using System.Collections.Generic;

namespace ConsoleApp
{
    public class ApplicationRole
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<ApplicationUserInRole> UserInRoles { get; set; } 
    }

}
