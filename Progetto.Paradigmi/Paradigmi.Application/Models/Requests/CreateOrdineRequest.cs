using Paradigmi.Models.Entities;

namespace Paradigmi.Application.Models.Requests;

public class CreateOrdineRequest
{
    public Utente utente { get; set; } = null!;
    public List<PortataOrdinata> portateOrdinate { get; set; }
    public Address IndirizzoConsegna { get; set; } = null!;



}