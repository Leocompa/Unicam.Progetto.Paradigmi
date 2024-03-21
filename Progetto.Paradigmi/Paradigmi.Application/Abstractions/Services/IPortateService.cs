using Paradigmi.Application.Models.Requests;
using Paradigmi.Models.Entities;

namespace Paradigmi.Application.Abstractions.Services;

public interface IPortateService
{
    List<PortataOrdinata> GetPortateOrdine(int idOrdine);
    Portata CreaPortata(string nome, decimal prezzo, Tipologia tipo);
}