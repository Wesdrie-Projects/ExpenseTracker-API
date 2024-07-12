using API.Infrastructure.Controller;
using API.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Features.Authentication;

[AllowAnonymous]
public class AuthenticationController : ExpenseBaseController
{
    public AuthenticationController(ExpenseContext context) : base(context) { }

    [HttpPost]
    public async Task<IActionResult> LoginAsync(LoginRequestVm request, [FromServices] UserManager<IdentityUser> userManager, [FromServices] IConfiguration configuration)
    {
        var user = await userManager.FindByEmailAsync(request.Username);

        if (user != null && await userManager.CheckPasswordAsync(user, request.Password))
        {
            var token = GenerateJwtToken(user, configuration);
            return Ok(token);
        }

        return Unauthorized();
    }

    private static string GenerateJwtToken(IdentityUser user, IConfiguration configuration)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(configuration["Jwt:Key"]);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new(ClaimTypes.NameIdentifier, user.Id),
                new(ClaimTypes.Name, user.UserName),
            }),
            Expires = DateTime.UtcNow.AddHours(24),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Issuer = configuration["Jwt:Issuer"],
            Audience = configuration["Jwt:Audience"],
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}

public record LoginRequestVm
{
    public required string Username { get; init; }
    public required string Password { get; init; }
}