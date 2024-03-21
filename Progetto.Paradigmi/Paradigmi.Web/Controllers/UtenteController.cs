using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Paradigmi.Application.Abstractions.Services;
using Paradigmi.Application.Factories;
using Paradigmi.Application.Models.Requests;
using Paradigmi.Application.Responses;
using Paradigmi.Models.Entities;

namespace Paradigmi.Web.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class UtenteController : ControllerBase
{
    private readonly IUtenteService _utenteService;

    public UtenteController(IUtenteService utenteService)
    {
        _utenteService = utenteService;
    }

    [HttpPost]
    [Route("new")]
    public async Task<IActionResult> CreateUtente(CreateUtenteRequest request, Ruolo ruolo)
    {
        //var claimsIdentity = this.User.Identity as ClaimsIdentity;
        //string idUtente = claimsIdentity.Claims.First(claim => claim.Type == "id_utente").Value;
        //TODO parte di validazione

        var utente = request.ToEntity(ruolo);
        await _utenteService.AddUtenteAsync(utente);

        var response = new CreateUtenteResponse();
        response.Utente = new Application.Models.Dtos.UtenteDto(utente);
        return Ok(ResponseFactory.WithSuccess(response));
    }
    
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
}