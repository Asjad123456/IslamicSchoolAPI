using IslamicSchool.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IslamicSchool.Controllers
{
    public class DeanController : BaseController
    {
        private readonly UserManager<AppUser> userManager;
        public DeanController(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
        }

        /*[Authorize(Policy ="RequireDeanRole")]*/
        [HttpGet("users-with-roles")]
        public async Task<IActionResult> GetUsersWithRoles()
        {
            var users = await userManager.Users
                .Include(r => r.UserRoles)
                .ThenInclude(r => r.Role)
                .OrderBy(u => u.UserName)
                .Select(u => new 
                {
                    u.Id,
                    UserName = u.UserName,
                    Roles = u.UserRoles.Select(r => r.Role.Name).ToList()
                })
                .ToListAsync();

            return Ok(users);
        }
        [HttpPost("edit-roles/{userName}")]
        public async Task<ActionResult> EditRoles(string userName, [FromQuery] string roles)
        {
            var selectedRoles = roles.Split(",").ToArray();

            var user = await userManager.FindByNameAsync(userName);

            if (user == null) return NotFound("Could not find user");

            var userRoles = await userManager.GetRolesAsync(user);

            if (userRoles == null) return NotFound("UserRoles not found");

            var result = await userManager.AddToRolesAsync(user, selectedRoles.Except(userRoles));

            if (!result.Succeeded) return BadRequest("Failed to add to roles");

        result = await userManager.RemoveFromRolesAsync(user, userRoles.Except(selectedRoles));

            if (!result.Succeeded) return BadRequest("Failed to remove from roles");

            return Ok(userRoles);
        }
        [Authorize(Policy = "RequireAdminRole")]
        [HttpGet("admin-board")]
        public ActionResult GetAdminResponsibilities()
        {
            return Ok("DEAN and admins can see this");
        }
    }
}
