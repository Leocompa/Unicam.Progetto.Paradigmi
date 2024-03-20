using Paradigmi.Models.Entities;

namespace Paradigmi.Application.Abstractions.Services;

public interface IPortateService
{
    List<PortataOrdinata> GetPortateOrdine(int idOrdine);
}