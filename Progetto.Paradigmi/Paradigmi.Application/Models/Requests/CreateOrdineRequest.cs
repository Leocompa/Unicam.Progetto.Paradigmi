using Paradigmi.Models.Entities;

namespace Paradigmi.Application.Models.Requests;

public class CreateOrdineRequest
{
    public string emailUtente { get; set; } = null!;
    public List<CreatePortateOrdinateRequest> portateOrdinate { get; set; }
    public Address? IndirizzoConsegna { get; set; } = null!;



}