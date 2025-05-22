using DemoMVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Demo_MVC.Models.ViewModels;

namespace DemoMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            var userRolesVM = new List<UserWithRoleVM>();
            
            foreach (var user in users)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var userWithRole = new UserWithRoleVM
                {
                    User = user,
                    Roles = userRoles
                };
                userRolesVM.Add(userWithRole);
            }
            
            return View(userRolesVM);
        }        // GET: Account/AssignRole/id
        public async Task<IActionResult> AssignRole(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var userRoles = await _userManager.GetRolesAsync(user);
            var model = new AssignRoleVM
            {
                UserId = id,
                UserName = user.UserName ?? "",
                Email = user.Email ?? "",
                Roles = new List<RoleSelection>()
            };

            var roles = await _roleManager.Roles.ToListAsync();
            foreach (var role in roles)
            {
                model.Roles.Add(new RoleSelection
                {
                    RoleId = role.Id,
                    RoleName = role.Name ?? "",
                    IsSelected = userRoles.Contains(role.Name ?? "")
                });            };
            return View(model);
        }        [HttpPost]
        public async Task<IActionResult> AssignRole(string userId, List<string> selectedRoles)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    return NotFound();
                }
                var userRoles = await _userManager.GetRolesAsync(user);
                
                // Thêm các vai trò được chọn mà chưa có
                if (selectedRoles != null)
                {
                    foreach (var role in selectedRoles)
                    {
                        if (!userRoles.Contains(role))
                        {
                            await _userManager.AddToRoleAsync(user, role);
                        }
                    }
                }
                
                // Xóa các vai trò hiện có mà không được chọn
                foreach (var role in userRoles)
                {
                    if (selectedRoles == null || !selectedRoles.Contains(role))
                    {
                        await _userManager.RemoveFromRoleAsync(user, role);
                    }
                }

                return RedirectToAction("Index", "Account");
            }
            
            // If we reached here, something failed, rebuild the model and return
            var userData = await _userManager.FindByIdAsync(userId);
            if (userData == null)
            {
                return NotFound();
            }
            
            var model = new AssignRoleVM
            {
                UserId = userId,
                UserName = userData.UserName ?? "",
                Email = userData.Email ?? "",
                Roles = new List<RoleSelection>()
            };
            
            var allRoles = await _roleManager.Roles.ToListAsync();
            var currentUserRoles = await _userManager.GetRolesAsync(userData);
            
            foreach (var role in allRoles)
            {
                model.Roles.Add(new RoleSelection
                {
                    RoleId = role.Id,
                    RoleName = role.Name ?? "",
                    IsSelected = selectedRoles?.Contains(role.Name ?? "") ?? false
                });
            }
            
            return View(model);
        }
    }
}