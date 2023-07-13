using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PledgeVault.Core.Contracts.Web;
using PledgeVault.Core.Dtos.Requests.Authentication;
using PledgeVault.Core.Dtos.Responses;
using PledgeVault.Core.Models;
using PledgeVault.Core.Regex;
using PledgeVault.Core.Security;
using PledgeVault.Persistence;
using PledgeVault.Persistence.Extensions;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PledgeVault.Api.Controllers;

[IgnoreAntiforgeryToken]
[Route("api/login")]
[ApiController]
public sealed class LoginController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly IContext _context;
    private readonly PledgeVaultContext _dbContext;

    public LoginController(IConfiguration configuration, IContext context, PledgeVaultContext dbContext)
    {
        _configuration = configuration;
        _context = context;
        _dbContext = dbContext;
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
        var id = await _context.GetCurrentUserId();
        return id is not null ? Ok($"Your ID is: {id}") : Unauthorized();
    }

    private string GenerateJwtToken(User user)
    {
        var credentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!)), SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, user.Email),
            new(ClaimTypes.NameIdentifier, user.Id.ToString())
        };

        var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Issuer"], claims, expires: DateTime.Now.AddMinutes(120), signingCredentials: credentials);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private async Task<User> AuthenticateUser(UserLoginRequest request)
    {
        // Get the user based on either Username or Email.
        var user = EmailRegex.IsValidEmail(request.UsernameOrEmail) ?
            await _dbContext.Users.WithOnlyActiveEntities().SingleOrDefaultAsync(x => x.Email == request.UsernameOrEmail)
                : await _dbContext.Users.WithOnlyActiveEntities().SingleOrDefaultAsync(x => x.Username == request.UsernameOrEmail);

        if (user is null) return null;

        return AuthenticationManager.IsPasswordMatch(request.Password, user.HashedPassword) ? user : null;
    }
}