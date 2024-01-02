using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Notes.WebApi.Controllers;
using Notes.WebApi.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Identity.Controllers;

[ApiController]
[ApiVersionNeutral]
[Produces("application/json")]
[Route("api/[controller]")]
public class AuthController : BaseController
{
    private readonly JWTSettings _options;

    public AuthController(IOptions<JWTSettings> options, SignInManager<IdentityUser> signInManager,
        UserManager<IdentityUser> userManager) : base(userManager, signInManager) => _options = options.Value;

    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] UserModel user)
    {
        var data = new IdentityUser { UserName = user.UserName };

        var result = await _userManager.CreateAsync(data, user.Password);

        if (result.Succeeded)
        {
            await _signInManager.SignInAsync(data, isPersistent: false);

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName)
            };

            await _userManager.AddClaimsAsync(data, claims);
        }
        else
        {
            return BadRequest();
        }

        return Ok();
    }

    [HttpPost("SignIn")]
    [Produces("text/plain")]
    public async Task<IActionResult> SignIn(UserModel user)
    {
        var systemUser = await _userManager.FindByNameAsync(user.UserName);

        var res = await _signInManager.PasswordSignInAsync(systemUser, user.Password, false, false);

        if (res.Succeeded)
        {
            IEnumerable<Claim> claims = await _userManager.GetClaimsAsync(systemUser);
            var token = GetToken(systemUser, claims);

            return Ok(token);
        }

        return BadRequest();
    }

    private string GetToken(IdentityUser user, IEnumerable<Claim> principal)
    {
        List<Claim> claims = principal.ToList();
        claims.Add(new Claim(ClaimTypes.Name, user.UserName));

        var signinKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));

        var jwt = new JwtSecurityToken(
                issuer: _options.Issuer,
                audience: _options.Audience,
                claims: claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromDays(15)),
                notBefore: DateTime.UtcNow,
                signingCredentials: new SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256)
            );

        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }
}
