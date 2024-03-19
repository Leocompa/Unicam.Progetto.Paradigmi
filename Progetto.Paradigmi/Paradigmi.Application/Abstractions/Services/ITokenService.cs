using Paradigmi.Application.Models.Requests;

namespace Paradigmi.Application.Abstractions.Services;

public interface ITokenService
{
    string CreateToken(CreateTokenRequest request);
}