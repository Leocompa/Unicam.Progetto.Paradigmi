using Paradigmi.Application.Abstractions.Services;
using Paradigmi.Models.Entities;
using Paradigmi.Models.Repositories;

namespace Paradigmi.Application.Services;

public class PortateService : IPortateService
{
    
    private readonly PortataRepository _portataRepository;

    public PortateService(PortataRepository portataRepository)
    {
        _portataRepository = portataRepository;
    }
    public Portata CreaPortata(string nome, decimal prezzo, Tipologia tipo)
    {
        //TODO controllare se nome portata già esistente
        var portata = new Portata(nome, prezzo, tipo);
        
        _portataRepository.Aggiungi(portata);
        _portataRepository.Save();

        return portata;
    }

    public List<Portata> GetPortate(string? nome, Tipologia? tipologia)
    {
       return _portataRepository.GetPortate(nome,tipologia);
    }
    
}