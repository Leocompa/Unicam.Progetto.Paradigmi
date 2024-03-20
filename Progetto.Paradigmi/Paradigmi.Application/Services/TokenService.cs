using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Paradigmi.Application.Abstractions.Services;
using Paradigmi.Application.Models.Requests;
using Paradigmi.Application.Options;

namespace Paradigmi.Application.Services;

public class TokenService : ITokenService
{
    private readonly JwtAuthenticationOption _jwtAuthenticationOption;

    public TokenService(IOptions<JwtAuthenticationOption> jwtAuthOptions)
    {
        _jwtAuthenticationOption = jwtAuthOptions.Value;

    }
    
    public string CreateToken(CreateTokenRequest request)
    {
        var claims = new List<Claim>();
        //TODO
        claims.Add(new Claim("id_utente", "1"));
        claims.Add(new Claim("email", request.Email));
        claims.Add(new Claim("password", request.Password));

        var securityKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_jwtAuthenticationOption.Key)
            );
        
        var credentials = new SigningCredentials(securityKey,
            SecurityAlgorithms.HmacSha256);
        
        var securityToken = new JwtSecurityToken(_jwtAuthenticationOption.Issuer
            , null
            , claims
            , expires: DateTime.Now.AddMinutes(30)
            ,signingCredentials: credentials
            );
        var token = new JwtSecurityTokenHandler().WriteToken(securityToken);
        
        return token;
    }
}