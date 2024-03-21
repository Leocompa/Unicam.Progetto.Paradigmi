using Paradigmi.Models.Entities;

namespace Paradigmi.Application.Abstractions.Services;

public interface IPortateOrdinateService
{
    List<PortataOrdinata> GetPortateOrdine(int idOrdine);
    
    decimal getCostoPortata(int idOrdine, string nomePortata);
}