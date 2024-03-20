using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Paradigmi.Application.Abstractions.Services;
using Paradigmi.Application.Factories;
using Paradigmi.Application.Models.Requests;
using Paradigmi.Application.Responses;

namespace Paradigmi.Web.Controllers;
[ApiController]
[Route("api/v1/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class OrdineController : ControllerBase
{
    private readonly IOrdineService _ordineService;
    

    public OrdineController(IOrdineService ordineService)
    {
        _ordineService = ordineService;
    }

    [HttpPost]
    [Route("new")]
    public async Task<IActionResult> CreateOrdine(CreateOrdineRequest request)
    {
        //TODO parte di validazione

        decimal costoTotale = 0; 
        int idOrdine = _ordineService.AddOrdine(request.utente, request.portateOrdinate,request.IndirizzoConsegna, out costoTotale);

        var response = new CreateOrdineResponse();
        response.Ordine = new Application.Models.Dtos.OrdineDto(_ordineService.GetOrdine(idOrdine)!);
        return Ok(ResponseFactory.WithSuccess(response));
    }
}