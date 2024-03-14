namespace Paradigmi.Models.Entities;

public class Ordine
{
    public Utente Utente { get; set; } = null!;
    public DateTime DataOrdine { get; set; }
    public long NumeroOrdine { get; set; }
    public Address IndirizzoConsegna { get; set; } = null!;
    public List<Portata> Type { get; set; } = null!;
}