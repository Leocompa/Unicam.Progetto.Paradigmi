using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Paradigmi.Application.Abstractions.Services;
using Paradigmi.Application.Models.Requests;
using Paradigmi.Application.Options;
using Paradigmi.Models.Entities;

namespace Paradigmi.Application.Services;

public class TokenService : ITokenService
{
    private readonly JwtAuthenticationOption _jwtAuthenticationOption;
    private readonly IUtenteService _utenteService;

    public TokenService(IOptions<JwtAuthenticationOption> jwtAuthOptions, IUtenteService utenteService)
    {
        _jwtAuthenticationOption = jwtAuthOptions.Value;
        _utenteService = utenteService;
    }
    
    public string CreateToken(CreateTokenRequest request)
    {
        var claims = new List<Claim>();
        
        claims.Add(new Claim("email", request.Email));
        Utente? utente = _utenteService.GetUtente(request.Email);
        claims.Add(new Claim("ruolo", utente == null ? Ruolo.Cliente.ToString() : utente.Ruolo.ToString()));

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