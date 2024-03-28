using Paradigmi.Models.Entities;

namespace Paradigmi.Application.Abstractions.Services;

public interface IPortateOrdinateService
{
    List<PortataOrdinata> GetPortateOrdine(int idOrdine);

    decimal GetCostoPortata(int idOrdine, string nomePortata);
}