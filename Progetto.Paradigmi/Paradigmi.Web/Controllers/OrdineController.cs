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
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class OrdineController : ControllerBase
{
    private readonly IOrdineService _ordineService;


    public OrdineController(IOrdineService ordineService)
    {
        _ordineService = ordineService;
    }

    [HttpPost]
    [Route("newOrdine")]
    public async Task<IActionResult> CreateOrdine(CreateOrdineRequest ordineRequest)
    {
        //TODO parte di validazione

        decimal costoTotale = 0;
        List<PortataOrdinata> portateOrdinate = new List<PortataOrdinata>();
        foreach (var portata in ordineRequest.portateOrdinate)
        {
            portateOrdinate.Add(portata.ToEntity());
        }

        int idOrdine = _ordineService.AddOrdine(ordineRequest.emailUtente, portateOrdinate,
            ordineRequest.IndirizzoConsegna, out costoTotale);

        var response = new CreateOrdineResponse();
        response.Ordine = new Application.Models.Dtos.OrdineDto(_ordineService.GetOrdine(idOrdine)!);
        return Ok(ResponseFactory.WithSuccess(response));
    }

    [HttpPost]
    [Route("get/StoricoOrdini")]
    public async Task<IActionResult> GetOrdini(CreateStoricoRequest storicoRequest)
    {
        var claimsIdentity = User.Identity as ClaimsIdentity;
        string claimRuolo = claimsIdentity.Claims.First(claim => claim.Type == "ruolo").Value;
        var ruolo = RuoloExtensions.AsRuolo(claimRuolo);
        if (ruolo == Ruolo.Cliente)
        {
            if (storicoRequest.EmailUtenteCercato != null)
            {
                if (storicoRequest.EmailUtenteCercato !=
                    claimsIdentity.Claims.First(claim => claim.Type == "email").Value)
                {
                    return BadRequest(ResponseFactory.WithError(
                        "devi essere un amministratore per ottenere lo storico di un utente specifico"));
                }
            }
            else
            {
                storicoRequest.EmailUtenteCercato = claimsIdentity.Claims.First(claim => claim.Type == "email").Value;
            }
        }

        int totalNum = 0;
        var response = _ordineService.GetStoricoOrdini(storicoRequest.PaginaCorrente, storicoRequest.RighePerPagina,
            ruolo, storicoRequest.DataInizio, storicoRequest.DataFine, storicoRequest.EmailUtenteCercato,
            out totalNum);
        return Ok(ResponseFactory.WithSuccess(response));
    }
}