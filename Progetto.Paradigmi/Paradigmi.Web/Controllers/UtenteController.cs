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
        var claimsIdentity = User.Identity as ClaimsIdentity;
        var claimRuolo = claimsIdentity.Claims.FirstOrDefault(claim => claim.Type == "ruolo");
        if (ruolo == Ruolo.Amministratore)
        {
            if (claimRuolo != null)
            {
                if (claimRuolo.Value == Ruolo.Amministratore.ToString())
                {
                    var utenteAmministratore = request.ToEntity(ruolo);
                    _utenteService.AddUtente(utenteAmministratore);

                    var responseAmministratore = new CreateUtenteResponse();
                    responseAmministratore.Utente = new Application.Models.Dtos.UtenteDto(utenteAmministratore);
                    return Ok(ResponseFactory.WithSuccess(responseAmministratore));
                }
            }
            else
            {
                return BadRequest(
                    ResponseFactory.WithError(
                        "devi essere autenticato come amministratore per creare un nuovo amministratore"));
            }
        }

        var utente = request.ToEntity(ruolo);
        _utenteService.AddUtente(utente);

        var response = new CreateUtenteResponse();
        response.Utente = new Application.Models.Dtos.UtenteDto(utente);
        return Ok(ResponseFactory.WithSuccess(response));
    }
}