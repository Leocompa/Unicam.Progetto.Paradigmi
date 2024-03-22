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
    public IActionResult CreateOrdine(CreateOrdineRequest ordineRequest)
    {
        int pastoCompleto = 0;
        decimal costoTotaleScontato = 0;
        decimal costoTotale = 0;
        List<PortataOrdinata> portateOrdinate = new List<PortataOrdinata>();
        foreach (var portata in ordineRequest.portateOrdinate)
        {
            if (portata.Quantita <= 0)
            {
                return BadRequest(ResponseFactory.WithError($"la quantita' ordinata della portata {portata.NomePortata} deve essere maggiore di 0"));
            }
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
    public IActionResult GetOrdini(CreateStoricoRequest storicoRequest)
    {
        if (storicoRequest.RighePerPagina <= 0 )
        {
            return BadRequest(ResponseFactory.WithError("il numero di righe per pagine deve essere maggiore di 0"));
        }
        if (storicoRequest.DataInizio.Value.CompareTo(storicoRequest.DataFine.Value) > 0)
        {
            return BadRequest(ResponseFactory.WithError("La data di inizio è successiva alla data di fine"));
        }
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
                    var claim = claimsIdentity.Claims.FirstOrDefault(claim => claim.Type.Contains("email"));
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
                    var claim = claimsIdentity.Claims.FirstOrDefault(claim => claim.Type.Contains("email"));
                    if (claim != null)
                    {
                        storicoRequest.EmailUtenteCercato = claim.Value;
                    }
                    else
                    {
                        return BadRequest(ResponseFactory.WithError(
                            "email non trovata"));
                    }
                }
            }

            int totalNum = 0;
            var storicoResponse = new List<CreateStoricoOrdineResponse>();
            foreach (var ordine in _ordineService.GetStoricoOrdini(storicoRequest.PaginaCorrente,
                         storicoRequest.RighePerPagina,
                         ruolo, storicoRequest.DataInizio, storicoRequest.DataFine, storicoRequest.EmailUtenteCercato,
                         out totalNum))
            {
                storicoResponse.Add(new CreateStoricoOrdineResponse(ordine.ClienteEmail, ordine.DataOrdine,
                    ordine.NumeroOrdine, ordine.IndirizzoConsegna));
            }

            var response = new CreateStoricoOrdineNumPagineResponse(storicoResponse, storicoRequest.PaginaCorrente,
                totalNum / storicoRequest.RighePerPagina);
            return Ok(ResponseFactory.WithSuccess(response));
        }

        return BadRequest(ResponseFactory.WithError("Ruolo non trovato per l'utente."));
    }


    [HttpPost]
    [Route("getOrdineByNumeroOrdine")]
    public IActionResult GetOrdineByNumeroOrdine(int numeroOrdine)
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