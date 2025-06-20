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


        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Extract domain from email
            var emailDomain = model.Email.Split('@').LastOrDefault();

            if (emailDomain == null)
                return BadRequest("Invalid email format");

            // Lookup tenant by domain
            var tenant = await _userManager.Users
                .IgnoreQueryFilters()
                .Select(u => u.Tenant)
                .Distinct()
                .FirstOrDefaultAsync(t => t.EmailDomain == "@" + emailDomain);

            if (tenant == null)
                return Unauthorized("No tenant found for this email domain");

            // Find user by email and tenant
            var user = await _userManager.Users
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(u => u.NormalizedEmail == model.Email.ToUpper() && u.TenantId == tenant.Id);

            if (user == null)
                return Unauthorized("Invalid credentials");

            if (!user.EmailConfirmed)
                return BadRequest("Email not confirmed");

            if (!await _userManager.CheckPasswordAsync(user, model.Password))
                return BadRequest("Wrong login combination");

            // SignInManager login
            var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, true);

            if (result.IsLockedOut)
                return BadRequest("Account is locked");

            if (!result.Succeeded)
                return Unauthorized("Failed login");

            // Maak claims, incl. TenantId
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("tenant_id", user.TenantId.ToString())
            };

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
                authClaims.Add(new Claim(ClaimTypes.Role, role));

            var token = Token.GetToken(authClaims);

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            });
        }

        [AllowAnonymous]
        [Route("List")]
        [HttpGet]
        public async Task<ActionResult> GetList()
        {
            return Ok(await _userManager.Users.IgnoreQueryFilters().ToListAsync());
        }


        [Authorize]
        [Route("Current")]
        [HttpGet]
        public async Task<ActionResult> GetCurrentUser()
        {
            ClaimsPrincipal currentUser = this.User;
            string? currentId = currentUser.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(currentId))
                return Unauthorized("No valid user id found in current token.");


            CustomUser? user = await _userManager.FindByIdAsync(currentId);

            if (user == null)
            {
                return NotFound("User not found");
            }

            //TODO: Change this to DTO instead of entity
            return Ok(user);
        }
    }

}
