using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Paradigmi.Application.Abstractions.Services;
using Paradigmi.Application.Factories;
using Paradigmi.Application.Models.Dtos;
using Paradigmi.Application.Models.Requests;
using Paradigmi.Application.Models.Responses;
using Paradigmi.Application.Responses;
using Paradigmi.Application.Services;
using Paradigmi.Models.Entities;

namespace Paradigmi.Web.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class OrdineController : ControllerBase
{
    private readonly IOrdineService _ordineService;
    private readonly IPortateOrdinateService _portateOrdinateService;


    public OrdineController(IOrdineService ordineService, IPortateOrdinateService portateOrdinateService)
    {
        _ordineService = ordineService;
        _portateOrdinateService = portateOrdinateService;
    }

    [HttpPost]
    [Route("newOrdine")]
    public async Task<IActionResult> CreateOrdine(CreateOrdineRequest ordineRequest)
    {
        //TODO parte di validazione

        int pastoCompleto = 0;
        decimal costoTotaleScontato = 0;
        decimal costoTotale = 0;
        List<PortataOrdinata> portateOrdinate = new List<PortataOrdinata>();
        foreach (var portata in ordineRequest.portateOrdinate)
        {
            portateOrdinate.Add(portata.ToEntity());
        }

        int numeroOrdine = _ordineService.AddOrdine(ordineRequest.emailUtente, portateOrdinate,
            ordineRequest.IndirizzoConsegna, out costoTotaleScontato, out costoTotale, out pastoCompleto);

        List<CreatePortateOrdinateRigaResponse>
            portateOrdinateResponses = new List<CreatePortateOrdinateRigaResponse>();
        foreach (var portata in portateOrdinate)
        {
            portateOrdinateResponses.Add(new CreatePortateOrdinateRigaResponse(portata.PortataNome, portata.Quantita,
                _portateOrdinateService.getCostoPortata(numeroOrdine, portata.PortataNome)));
        }

        var response = new CreateOrdineResponse(numeroOrdine, ordineRequest.IndirizzoConsegna, costoTotale,
            costoTotaleScontato, pastoCompleto, portateOrdinateResponses);
        return Ok(ResponseFactory.WithSuccess(response));
    }

    [HttpPost]
    [Route("get/StoricoOrdini")]
    public async Task<IActionResult> GetOrdini(CreateStoricoRequest storicoRequest)
    {
        var claimsIdentity = User.Identity as ClaimsIdentity;
        var claimRuolo = claimsIdentity.Claims.FirstOrDefault(claim => claim.Type == "ruolo");
        if (claimRuolo != null)
        {
            string ruoloClaim = claimRuolo.Value;
            var ruolo = RuoloExtensions.AsRuolo(ruoloClaim);
            if (ruolo == Ruolo.Cliente)
            {
                if (storicoRequest.EmailUtenteCercato != null)
                {
                    var claim = claimsIdentity.Claims.FirstOrDefault(claim => claim.Type.Equals("email"));
                    if (claim == null)
                    {
                        return BadRequest(ResponseFactory.WithError("errore nullo"));
                    }

                    if (storicoRequest.EmailUtenteCercato != claim.Value)
                    {
                        return BadRequest(ResponseFactory.WithError(
                            "devi essere un amministratore per ottenere lo storico di un utente specifico"));
                    }
                }
                else
                {
                    var claim = claimsIdentity.Claims.FirstOrDefault(claim => claim.Type.Equals("email"));
                    if (claim!= null)
                    {
                        storicoRequest.EmailUtenteCercato = claim.Value;
                    }

                    return BadRequest(ResponseFactory.WithError(
                        "email non trovata"));
                }
            }

            int totalNum = 0;
            var response = _ordineService.GetStoricoOrdini(storicoRequest.PaginaCorrente, storicoRequest.RighePerPagina,
                ruolo, storicoRequest.DataInizio, storicoRequest.DataFine, storicoRequest.EmailUtenteCercato,
                out totalNum);
            return Ok(ResponseFactory.WithSuccess(response));
        }

        return BadRequest(ResponseFactory.WithError("Ruolo non trovato per l'utente."));
    }


    [HttpPost]
    [Route("getOrdineByNumeroOrdine")]
    public async Task<IActionResult> GetOrdineByNumeroOrdine(int numeroOrdine)
    {
        Ordine? ordine = _ordineService.GetOrdine(numeroOrdine);
        if (ordine == null)
        {
            return BadRequest(ResponseFactory.WithError("Numero ordine non valido"));
        }

        var response = new OrdineDto(ordine);
        return Ok(ResponseFactory.WithSuccess(response));
    }
}