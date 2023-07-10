using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PledgeVault.Core.Dtos.Requests.Authentication;
using PledgeVault.Core.Dtos.Responses;
using PledgeVault.Core.Models;
using PledgeVault.Core.Security;
using PledgeVault.Persistence;

namespace PledgeVault.Api.Controllers;

[Route("api/login")]
[ApiController]
public class LoginController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly PledgeVaultContext _context;

    public LoginController(IConfiguration configuration, PledgeVaultContext context)
    {
        _configuration = configuration;
        _context = context;
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
    {
        var user = await AuthenticateUser(request);
        if (user is null) return Unauthorized();
        return Ok(new LoginResponse { Token = GenerateJwtToken(user) });
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var user = await GetCurrentUser();
        return user is not null ? Ok($"Your email is: {user.Email}") : Unauthorized();
    }

    private async Task<User> GetCurrentUser()
    {
        var user = HttpContext.User.Identity as ClaimsIdentity;

        if (user is null) return null;

        return await _context.Users.SingleOrDefaultAsync(x => x.Email == user.Name)!;
    }

    private string GenerateJwtToken(User user)
    {
        var credentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!)), SecurityAlgorithms.HmacSha256);
        var claims = new List<Claim> { new(ClaimTypes.Name, user.Email) };
        var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Issuer"], claims, expires: DateTime.Now.AddMinutes(120), signingCredentials: credentials);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private async Task<User> AuthenticateUser(UserLoginRequest request)
    {
        var user = await _context.Users.SingleOrDefaultAsync(x => x.Email == request.Email);

        if (user is null) return null;

        var sss = AuthenticationManager.IsPasswordMatch(request.Password, user.HashedPassword);
        return AuthenticationManager.IsPasswordMatch(request.Password, user.HashedPassword) ? user : null;
    }
}