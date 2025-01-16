using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace SneakerSZN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        [HttpGet("info")]
        [Authorize]
        public List<string>? GetInfo()
        {
            List<string> info = new List<string>();

            string? id = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (id == null)
            {
                return null;
            }

            info.Add(id);
            info.AddRange(User.Claims.Where(c => c.Type == ClaimTypes.Email).Select(r => r.Value).ToList());

            return info;
        }

        [HttpGet("roles")]
        [Authorize]
        public IActionResult GetUserRoles()
        {
            var roles = ((ClaimsIdentity)User.Identity).Claims
                .Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value)
                .ToList();

           
            if (roles == null)
            {
                return NotFound();
            }

            return Ok(roles);
        }
    }
}
