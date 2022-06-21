using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ProjectX.Data.Entities;
using ProjectX.Dto;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProjectX.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet("roles")]
        public async Task<IActionResult> Role()
        {
            var result = await _roleManager.CreateAsync(new IdentityRole { Name = "Admin" });
            result = await _roleManager.CreateAsync(new IdentityRole { Name = "Member" });
            result = await _roleManager.CreateAsync(new IdentityRole { Name = "SuperAdmin" });

            return StatusCode(201);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            AppUser user = await _userManager.FindByNameAsync(registerDto.UserName);

            if (user != null)
            {
                return BadRequest("Ueser alredy exist!");
            }

            user = new AppUser
            {
                FullName = registerDto.FullName,
                UserName = registerDto.UserName,
                Email = registerDto.Email,
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            await _userManager.AddToRoleAsync(user, "Member");

            return StatusCode(201, user);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {

            AppUser user = await _userManager.FindByNameAsync(loginDto.UserName);
            if (user == null)
            {
                return NotFound();
            }

            List<Claim> claims = new List<Claim>
            {
                new Claim("FullName", user.FullName),
                new Claim(ClaimTypes.NameIdentifier, user.UserName),
            };

            var roles = await _userManager.GetRolesAsync(user);

            claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)).ToList());

            string secretKey = "840961ef-bdf3-478e-8120-b16f28aac6cc";
            SymmetricSecurityKey key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(secretKey));
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                expires:DateTime.Now.AddDays(1),
                signingCredentials: credentials,
                claims: claims
                );

            string token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);


            return Ok(new
            {
                token = token
            });
        }

    }
}
