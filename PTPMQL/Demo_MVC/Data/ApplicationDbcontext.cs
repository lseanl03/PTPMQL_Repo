using Microsoft.EntityFrameworkCore;
using Demo_MVC.Models;
using VicemMVIdentity.Models.Entities;
using DemoMVC.Models.Entities;
namespace DemoMVC.Data
{
    using Microsoft.EntityFrameworkCore;
    using Demo_MVC.Models;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using DemoMVC.Models;
    using Microsoft.AspNetCore.Identity;

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        //Ánh xạ class Student vào trong csdl => tạo ra bảng Students
        public DbSet<Student> student { get; set; }

        public DbSet<Demo_MVC.Models.Test> Test { get; set; } = default!;

        public DbSet<Demo_MVC.Models.Daily> Daily { get; set; } = default!;

        public DbSet<Demo_MVC.Models.HeThongPhanPhoi> HeThongPhanPhoi { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ApplicationUser>().ToTable("Users");
            builder.Entity<IdentityRole>().ToTable("Roles");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
        }

public DbSet<VicemMVIdentity.Models.Entities.MemberUnit> MemberUnit { get; set; } = default!;

public DbSet<DemoMVC.Models.Entities.Employee1> Employee1 { get; set; } = default!;
    }
}