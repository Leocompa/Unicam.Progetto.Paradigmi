using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Paradigmi.Application.Abstractions.Services;
using Paradigmi.Application.Factories;
using Paradigmi.Application.Models.Requests;
using Paradigmi.Application.Models.Responses;
using Paradigmi.Models.Entities;

namespace Paradigmi.Web.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class TokenController : Controller
{
    private readonly ITokenService _tokenService;

    public TokenController(ITokenService tokenService)
    {
        _tokenService = tokenService;
    }

    [HttpPost]
    [Route("create")]
    public IActionResult Create(CreateTokenRequest request)
    {
        if (request.Email.IsNullOrEmpty())
        {
            return BadRequest(ResponseFactory.WithError("Il campo email non puo' essere nullo"));
        }
        string token = _tokenService.CreateToken(request);
        return Ok(ResponseFactory.WithSuccess(new CreateTokenResponse(token)));
    }
}