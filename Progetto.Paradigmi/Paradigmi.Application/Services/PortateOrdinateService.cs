using Paradigmi.Application.Abstractions.Services;
using Paradigmi.Application.Models.Requests;
using Paradigmi.Models.Entities;
using Paradigmi.Models.Repositories;

namespace Paradigmi.Application.Services;

public class PortateOrdinateService : IPortateService
{
    private readonly PortateOrdinateRepository _portateOrdinateRepository;
    private readonly PortataRepository _portataRepository;
    public PortateOrdinateService(PortateOrdinateRepository portateOrdinateRepository, PortataRepository portataRepository)
    {
        _portateOrdinateRepository = portateOrdinateRepository;
        _portataRepository = portataRepository;
    }

    public List<PortataOrdinata> GetPortateOrdine(int idOrdine)
    {
        return _portateOrdinateRepository.getPortateOrdinate(idOrdine);
    }

    public Portata CreaPortata(string nome, decimal prezzo, Tipologia tipo)
    {
        //TODO controllare se nome portata già esistente
        var portata = new Portata(nome, prezzo, tipo);
        Console.WriteLine(nome);
        Console.WriteLine(prezzo);
        Console.WriteLine(tipo);
        _portataRepository.Aggiungi(portata);
        _portataRepository.Save();

        return portata;
    }

    public decimal getCostoPortata(int idOrdine,string nomePortata)
    {
        return _portateOrdinateRepository.getCosto(idOrdine,nomePortata);
    }

  
}