using Paradigmi.Application.Abstractions.Services;
using Paradigmi.Models.Entities;
using Paradigmi.Models.Repositories;

namespace Paradigmi.Application.Services;

public class PortateOrdinateService : IPortateService
{
    private readonly PortateOrdinateRepository _portateOrdinateRepository;
    private readonly PortataRepository _portataRepository;
    public PortateOrdinateService(PortateOrdinateRepository portateOrdinateRepository)
    {
        _portateOrdinateRepository = portateOrdinateRepository;
    }

    public List<PortataOrdinata> GetPortateOrdine(int idOrdine)
    {
        return _portateOrdinateRepository.getPortateOrdinate(idOrdine);
    }

    public decimal getCostoPortata(int idOrdine,string nomePortata)
    {
        return _portateOrdinateRepository.getCosto(idOrdine,nomePortata);
    }
}