using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Demo_MVC.Models.ViewModels
{
    public class AssignRoleVM
    {
        public string? UserId { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public List<RoleSelection> Roles { get; set; } = new List<RoleSelection>();
    }

    public class RoleSelection
    {
        public string? RoleId { get; set; }
        public string? RoleName { get; set; }
        public bool IsSelected { get; set; }
    }
}
