using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Paradigmi.Application.Abstractions.Services;
using Paradigmi.Application.Factories;
using Paradigmi.Application.Models.Responses;
using Paradigmi.Models.Entities;

namespace Paradigmi.Web.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class PortataOrdinataController : ControllerBase
{
    private readonly IPortateOrdinateService _portateOrdinateService;
    private readonly IOrdineService _ordineService;

    public PortataOrdinataController(IPortateOrdinateService portateOrdinateService, IOrdineService ordineService)
    {
        _portateOrdinateService = portateOrdinateService;
        _ordineService = ordineService;
    }

    [HttpPost]
    [Route("getPortateByNumeroOrdine")]
    public async Task<IActionResult> GetPortateByNumeroOrdine(int numeroOrdine)
    {
        List<PortataOrdinata> portateOrdinate = new List<PortataOrdinata>();

        var claimsIdentity = User.Identity as ClaimsIdentity;
        var claimRuolo = claimsIdentity.Claims.FirstOrDefault(claim => claim.Type == "ruolo");
        if (claimRuolo == null)
        {
            return BadRequest(ResponseFactory.WithError("Ruolo non trovato per l'utente."));
        }

        string ruoloClaim = claimRuolo.Value;
        var ruolo = RuoloExtensions.AsRuolo(ruoloClaim);
        if (ruolo == Ruolo.Amministratore)
        {
            portateOrdinate = _portateOrdinateService.GetPortateOrdine(numeroOrdine);
        }
        else
        {
            Ordine? ordine = _ordineService.GetOrdine(numeroOrdine);
            if (ordine != null)
            {
                var claimEmail = claimsIdentity.Claims.FirstOrDefault(claim => claim.Type == "email");
                if (claimEmail == null)
                {
                    return BadRequest(ResponseFactory.WithError("email non trovata per l'utente."));
                }
                string email = claimEmail.Value;
                if(email.ToLower().Equals(ordine.ClienteEmail.ToLower()))
                {
                    portateOrdinate = _portateOrdinateService.GetPortateOrdine(numeroOrdine);
                }
            }
        }
        List<CreatePortateOrdinateResponse> portateOrdinateResponses = new List<CreatePortateOrdinateResponse>();
        foreach (var portata in portateOrdinate)
        {
            portateOrdinateResponses.Add(new CreatePortateOrdinateResponse(portata.PortataNome, portata.Quantita));
        }
        
        return Ok(ResponseFactory.WithSuccess(portateOrdinateResponses));
    }
    
}