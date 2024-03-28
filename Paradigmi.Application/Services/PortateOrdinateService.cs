using Paradigmi.Application.Abstractions.Services;
using Paradigmi.Models.Entities;
using Paradigmi.Models.Repositories;

namespace Paradigmi.Application.Services;

public class PortateOrdinateService : IPortateOrdinateService
{
    private readonly PortateOrdinateRepository _portateOrdinateRepository;

    public PortateOrdinateService(PortateOrdinateRepository portateOrdinateRepository)
    {
        _portateOrdinateRepository = portateOrdinateRepository;
    }

    public List<PortataOrdinata> GetPortateOrdine(int numeroOrdine)
    {
        return _portateOrdinateRepository.GetPortateOrdinate(numeroOrdine);
    }


    public decimal GetCostoPortata(int numeroOrdine, string nomePortata)
    {
        return _portateOrdinateRepository.GetCosto(numeroOrdine, nomePortata);
    }
}