using Paradigmi.Models.Entities;

namespace Paradigmi.Application.Models.Responses;

public class CreateTokenResponse
{
    public string Token { get; set; } = string.Empty;
    public string Messaggio { get; set; } = string.Empty;
    
    
    public CreateTokenResponse(string token, string messaggio)
    {
        Token = token;
        Messaggio = messaggio;
    }
}