using EduNova.Core.DTO.Auth;
using EduNova.Infrastructure.Entities;
using EduNova.Infrastructure.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace EduNova.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private UserManager<CustomUser> _userManager;
        private readonly SignInManager<CustomUser> _signInManager;
        private RoleManager<IdentityRole> _roleManager;

        public AuthController(UserManager<CustomUser> userManager, SignInManager<CustomUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }


        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null && !user.EmailConfirmed)
            {
                ModelState.AddModelError("message", "Emailadres not yet confirmed.");
                return BadRequest(model);
            }
            if (await _userManager.CheckPasswordAsync(user, model.Password) == false)
            {
                ModelState.AddModelError("message", "Wrong logincombination!");
                return BadRequest(model);
            }

            var result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, false, true);

            if (result.IsLockedOut)
                ModelState.AddModelError("message", "Account blocked!");

            if (result.Succeeded)
            {
                var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

                var userRoles = await _userManager.GetRolesAsync(user);
                if (userRoles != null)
                {
                    foreach (var userRole in userRoles)
                        authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var token = Token.GetToken(authClaims);

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }

            ModelState.AddModelError("message", "Failed login attempt");
            return Unauthorized(ModelState);
        }

        [Authorize]
        [Route("List")]
        [HttpGet]
        public async Task<ActionResult> GetLijst()
        {
            return Ok(await _userManager.Users.ToListAsync());
        }
    }

}
