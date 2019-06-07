namespace ConsoleApp
{
    public class ApplicationUserInRole
    {
        public int UserId { get; set; }
        public ApplicationUser User { get; set; }

        public int RoleId { get; set; }
        public ApplicationRole Role { get; set; }
    }

}
