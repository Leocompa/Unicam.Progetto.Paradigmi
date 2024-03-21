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
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class PortataController : ControllerBase
{
    private readonly IPortateService _portateService;

    public PortataController(IPortateService portateService)
    {
        _portateService = portateService;
    }

    [HttpPost]
    [Route("newPortata")]
    public async Task<IActionResult> CreatePortata(CreatePortataRequest portataRequest, Tipologia tipologia)
    {
        //TODO parte di validazione
        var portata = _portateService.CreaPortata(portataRequest.Nome, portataRequest.Prezzo, tipologia);

        var response = new CreatePortataResponse();
        response.Portata = new PortataDto(portata);
        return Ok(ResponseFactory.WithSuccess(response));
    }
}
