using Paradigmi.Models.Entities;

namespace Paradigmi.Application.Abstractions.Services;

public interface IPortateService
{
    Portata CreaPortata(string nome, decimal prezzo, Tipologia tipo);
    List<Portata> GetPortate(string? nome, Tipologia? tipologia);
}