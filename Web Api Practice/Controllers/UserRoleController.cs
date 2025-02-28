using Entities;
using JWTAuthentication.NET8._0.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Web_Api_Practice.CustomHandler;
using WebApi.DAL.DBContext;

namespace Web_Api_Practice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRoleController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;

        public UserRoleController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        // GET: api/UserRoles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> GetUserRoles()
        {
            var roles = await _roleManager.Roles.Select(ur => ur.Name).ToListAsync();
            return Ok(roles);
        }

        // POST: api/UserRoles
        [HttpPost]
        public async Task<ActionResult<string>> PostUserRole([FromBody] string roleName)
        {
            if (string.IsNullOrWhiteSpace(roleName))
            {
                return BadRequest("Role name cannot be empty.");
            }

            if (await _roleManager.RoleExistsAsync(roleName))
            {
                return Conflict("Role already exists.");
            }

            var result = await _roleManager.CreateAsync(new IdentityRole(roleName));
            if (result.Succeeded)
            {
                return Ok($"Role '{roleName}' has been added.");
            }

            return StatusCode(StatusCodes.Status500InternalServerError, "Error creating role.");
        }

        [HttpPost("assign-permission")]
        public async Task<ActionResult> AssignPermissionToRole([FromBody] AssignPermissionModel model)
        {
            var role = await _roleManager.FindByNameAsync(model.RoleName);
            if (role == null)
            {
                return NotFound("Role not found.");
            }

            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            var result = await _userManager.AddClaimAsync(user, new Claim("Permission", model.Permission));
            var result1 = await _roleManager.AddClaimAsync(role, new Claim("Permission", model.Permission));

            if (result.Succeeded)
            {
                return Ok($"Permission '{model.Permission}' has been assigned to role '{model.RoleName}' for user '{model.UserName}'.");
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error assigning permission.");
            }
        }
    }


}
