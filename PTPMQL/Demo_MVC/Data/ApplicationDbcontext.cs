using Microsoft.EntityFrameworkCore;
using Demo_MVC.Models;
namespace DemoMVC.Data
{
    using Microsoft.EntityFrameworkCore;
    using Demo_MVC.Models;

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        //Ánh xạ class Student vào trong csdl => tạo ra bảng Students
        public DbSet<Student> student { get; set; }

public DbSet<Demo_MVC.Models.Test> Test { get; set; } = default!;

public DbSet<Demo_MVC.Models.Daily> Daily { get; set; } = default!;

public DbSet<Demo_MVC.Models.HeThongPhanPhoi> HeThongPhanPhoi { get; set; } = default!;
    }
}