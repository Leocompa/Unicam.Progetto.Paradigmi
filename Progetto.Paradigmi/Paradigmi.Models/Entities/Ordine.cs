using System.ComponentModel.DataAnnotations;

namespace Paradigmi.Models.Entities;

public class Ordine
{
    
    public string ClienteEmail { get; set; }
    public DateTime DataOrdine { get; set; }

    public int NumeroOrdine { get; set; }
    public Address IndirizzoConsegna { get; set; } = null!;
    
    
    public ICollection<PortataOrdinata> PortateSelezionate { get; set; } = null!;
    public Utente Utente { get; set; } = null!;
}
