using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Paradigmi.Application.Abstractions.Services;
using Paradigmi.Application.Factories;
using Paradigmi.Application.Models.Dtos;
using Paradigmi.Application.Models.Requests;
using Paradigmi.Application.Models.Responses;
using Paradigmi.Models.Entities;

namespace Paradigmi.Web.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class PortataController : ControllerBase
{
    private readonly IPortateService _portateService;

    public PortataController(IPortateService portateService)
    {
        _portateService = portateService;
    }

    [HttpPost]
    [Route("newPortata")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public IActionResult CreatePortata(CreatePortataRequest portataRequest, Tipologia tipologia)
    {
        var claimsIdentity = User.Identity as ClaimsIdentity;
        string claimRuolo = claimsIdentity.Claims.First(claim => claim.Type == "ruolo").Value;
        var ruolo = RuoloExtensions.AsRuolo(claimRuolo);
        if (ruolo == Ruolo.Amministratore)
        {
            var portata = _portateService.CreaPortata(portataRequest.Nome, portataRequest.Prezzo, tipologia);

            var response = new CreatePortataResponse();
            response.Portata = new PortataDto(portata);
            return Ok(ResponseFactory.WithSuccess(response));
        }

        return BadRequest(ResponseFactory.WithError(
            "devi essere un amministratore per creare una nuova portata"));
    }

    [HttpPost]
    [Route("getPortate")]
    public IActionResult GetPortate(string? nome, Tipologia? tipologia)
    {
        List<Portata> portate = _portateService.GetPortate(nome, tipologia);
        return Ok(ResponseFactory.WithSuccess(portate));
    }
}