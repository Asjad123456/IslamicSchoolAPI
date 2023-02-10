using AutoMapper;
using IslamicSchool.Data;
using IslamicSchool.DataTransferObjects;
using IslamicSchool.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace IslamicSchool.Controllers
{
    public class AccountController : BaseController
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly IMapper mapper;
        private readonly SymmetricSecurityKey key;

        public AccountController(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager, IMapper mapper,IConfiguration config)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.mapper = mapper;
            key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
        }
        [HttpPost("Registration")]
        public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userForRegistration)
        {
            if (userForRegistration == null || !ModelState.IsValid)
                return BadRequest();
            var user = mapper.Map<AppUser>(userForRegistration);

            var result = await userManager.CreateAsync(user, userForRegistration.Password);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);

                return BadRequest(new RegistrationResponseDto { Errors = errors });
            }
            var roleResult = await userManager.AddToRoleAsync(user, "NEWENTRY");
            if (!roleResult.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);

                return BadRequest(new RegistrationResponseDto { Errors = errors });
            }
            return StatusCode(201);

        }
        [HttpPost("login")]
        public async Task<ActionResult> Login(UserForAuthenticationDto userForAuthenticationDto)
        {
            var user = await userManager.Users
                .SingleOrDefaultAsync(x => x.UserName == userForAuthenticationDto.UserName.ToLower());
            var returnuser = await userManager.Users.ToListAsync();
/*            var roles = userManager.GetRolesAsync(user);
*/
            if (user == null) return Unauthorized("UserName not Found");

            var result = await signInManager.CheckPasswordSignInAsync(user, userForAuthenticationDto.Password, false);

            if (!result.Succeeded) return Unauthorized();

            return Ok(new AuthResponseDto { IsAuthSuccessful = true, Token = await CreateToken(user), Roles = await ReturnRoles(user), AppUser = user});
        }
        private async Task<IList<string>> ReturnRoles(AppUser user)
        {
            var roles = await userManager.GetRolesAsync(user);
            string roleString = string.Join(", ", roles);
            return new List<string> { roleString };
        }
        private async Task<string> CreateToken(AppUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.UserName),
            };

            var roles = await userManager.GetRolesAsync(user);

            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role))); 

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = creds
            };
            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
