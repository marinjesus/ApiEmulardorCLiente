using ApiEmulardorCLiente.Controllers.Base;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;

namespace ApiEmulardorCLiente.Controllers;


[Route("api/Auth")]
[ApiController]
public class AuthController : BaseApiController
{

    public AuthController(IConfiguration configuration, IMapper mapper) :
        base(configuration, mapper)
    {

    }

    [HttpPost("Authenticate")]
    public async Task<IActionResult> Authenticate(dynamic Model)
    {
        var claims = new List<Claim> { new Claim(ClaimTypes.Name, "ApiSimula") };
        Dictionary<string, object> objModel = JsonConvert.DeserializeObject<Dictionary<string, object>>(Convert.ToString(Model));
        foreach (var item in objModel)
        {
            if (!item.Key.Contains("Key", StringComparison.OrdinalIgnoreCase))
            {
                string? ovalue = item.Value.ToString();
                if (ovalue == null)
                    ovalue = "";
                claims.Add(new Claim(item.Key, ovalue));
            }
        }
        var signingCredentials = GetSigningCredentials();
        var tokenOptions = GenerateTokenOptions(signingCredentials, claims);
        return Ok(new { Token = new JwtSecurityTokenHandler().WriteToken(tokenOptions) });
    }
    private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
    {
        var jwtSettings = _configuration.GetSection("JwtConfig");
        var tokenOptions = new JwtSecurityToken
        (
        claims: claims,
        expires: DateTime.Now.AddSeconds(Convert.ToDouble(jwtSettings["expiresIn"])),
        signingCredentials: signingCredentials
        );
        return tokenOptions;
    }

    private SigningCredentials GetSigningCredentials()
    {
        var jwtConfig = _configuration.GetSection("jwtConfig");
        var key = Encoding.UTF8.GetBytes(jwtConfig["Secret"]);
        var secret = new SymmetricSecurityKey(key);
        return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
    }

}
