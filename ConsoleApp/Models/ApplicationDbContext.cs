using Microsoft.EntityFrameworkCore;

namespace ConsoleApp
{
    public class ApplicationDbContext : DbContext
    {
        const string ConnectionString = "Persist Security Info=False;User ID=sa;Password=Pa$$w0rd;Initial Catalog=ApplicationTestDb;Server=192.168.1.227";

        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<ApplicationRole> ApplicationRole { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Application.ApplicationUser

            modelBuilder.Entity<ApplicationUser>()
                .ToTable("ApplicationUser", "Application")
                .HasKey(x => x.Id);

            modelBuilder.Entity<ApplicationUser>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<ApplicationUser>()
                .Property(x => x.Email)
                .HasMaxLength(150);

            modelBuilder.Entity<ApplicationUser>()
                .HasIndex(x => x.Email)
                .IsUnique();

            modelBuilder.Entity<ApplicationUser>()
                .Property(x => x.Name)
                .HasMaxLength(50);

            #endregion

            #region Application.ApplicationRole

            modelBuilder.Entity<ApplicationRole>()
                .ToTable("ApplicationRole", "Application")
                .HasKey(x => x.Id);

            modelBuilder.Entity<ApplicationRole>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<ApplicationRole>()
                .Property(x => x.Name)
                .HasMaxLength(50);

            modelBuilder.Entity<ApplicationRole>()
                .HasIndex(x => x.Name)
                .IsUnique();

            #endregion

            #region Application.ApplicationUserInRole

            modelBuilder.Entity<ApplicationUserInRole>()
                .ToTable("ApplicationUserInRole", "Application")
                .HasKey(x => new { x.UserId, x.RoleId });

            modelBuilder.Entity<ApplicationUserInRole>()
                .HasOne(x => x.User)
                .WithMany(x => x.UserInRoles)
                .HasForeignKey(x => x.UserId);

            modelBuilder.Entity<ApplicationUserInRole>()
                .HasOne(x => x.Role)
                .WithMany(x => x.UserInRoles)
                .HasForeignKey(x => x.RoleId);

            #endregion
        }
    }

}
