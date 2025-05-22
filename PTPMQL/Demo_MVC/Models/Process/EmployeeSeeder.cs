using Bogus;
using DemoMVC.Data;
using DemoMVC.Models.Entities;
using VicemMVIdentity.Models.Entities;

namespace VicemMVIdentity.Models.Process
{
    public class EmployeeSeeder
    {
        private readonly ApplicationDbContext _context;

        public EmployeeSeeder(ApplicationDbContext context)
        {
            _context = context;
        }

    public void SeedEmployees(int n)
    {
        // Kiểm tra xem đã có nhân viên trong DB chưa
        if (!_context.Employee1.Any())
        {
            var employees = GenerateEmployees(n);
            _context.Employee1.AddRange(employees);
            _context.SaveChanges();
        }
    }

        private List<Employee1> GenerateEmployees(int n)
        {
            var faker = new Faker<Employee1>()
                .RuleFor(e => e.FirstName, f => f.Name.FirstName())
                .RuleFor(e => e.LastName, f => f.Name.LastName())
                .RuleFor(e => e.Address, f => f.Address.FullAddress())
                .RuleFor(e => e.DateOfBirth, f => f.Date.Past(30, DateTime.Now.AddYears(-20)))
                .RuleFor(e => e.Position, f => f.Name.JobTitle())
                .RuleFor(e => e.Email, (f, e) => f.Internet.Email(e.FirstName, e.LastName))
                .RuleFor(e => e.HireDate, f => f.Date.Past(10));

            return faker.Generate(n);
        }
    }
}