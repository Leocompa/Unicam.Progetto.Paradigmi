using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices.JavaScript;

namespace Paradigmi.Models.Entities;

public class Ordine
{
    
    public string ClienteEmail { get; set; } = String.Empty;
    public DateOnly DataOrdine { get; set; }
    public int NumeroOrdine { get; set; }
    public Address IndirizzoConsegna { get; set; } = null!;
    
    public ICollection<PortataOrdinata> PortateSelezionate { get; set; } = null!;
    public Utente Utente { get; set; } = null!;

   
}
