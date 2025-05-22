using DemoMVC.Models;
using Microsoft.AspNetCore.Identity;

namespace Demo_MVC.Models.ViewModels
{
    public class UserWithRoleVM
    {
        public ApplicationUser? User { get; set; }
        public IList<string>? Roles { get; set; }
    }
}
