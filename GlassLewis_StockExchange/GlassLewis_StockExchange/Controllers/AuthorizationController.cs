using GlassLewis_StockExchange.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GlassLewis_StockExchange.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthorizationController(UserManager<IdentityUser> userManager, IConfiguration config) : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager = userManager;
    private readonly IConfiguration _config = config;

    [HttpPost(Name = "register")]
    public async Task<IActionResult> Register([FromBody] User model)
    {
        var user = new IdentityUser { UserName = model.Username, Email = model.Username };
        var result = await _userManager.CreateAsync(user, model.Password!);

        if (result.Succeeded)
        {
            return Ok(MessageConstants.RegistrationSuccessful);
        }

        return BadRequest(result.Errors);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] User model)
    {
        var user = await _userManager.FindByEmailAsync(model.Username!);

        if (user is not null && await _userManager.CheckPasswordAsync(user, model.Password!))
        {
            var token = GetJwtToken(user);
            return Ok(new { Token = token });
        }

        return Unauthorized(MessageConstants.LoginSuccessful);
    }

    private string GetJwtToken(IdentityUser user)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Email!),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.NameIdentifier, user.Id)
        };

        var issuer = _config[JwtAuthConstants.Issuer];
        var expiry = DateTime.Now.AddMinutes(double.Parse(_config[JwtAuthConstants.Expiry]!));
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config[JwtAuthConstants.Key]!));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(issuer: issuer, expires: expiry, claims: claims, signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
