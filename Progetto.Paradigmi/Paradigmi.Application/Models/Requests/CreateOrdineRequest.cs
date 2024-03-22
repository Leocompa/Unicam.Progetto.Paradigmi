using Paradigmi.Models.Entities;

namespace Paradigmi.Application.Models.Requests;

public class CreateOrdineRequest
{
    public string EmailUtente { get; set; } = null!;
    public List<CreatePortateOrdinateRequest> PortateOrdinate { get; set; }
    public Address? IndirizzoConsegna { get; set; } = null!;
}