namespace Paradigmi.Models.Entities;

public class PortataOrdinata
{
    public int id { get; set; }
    public Portata Piatto { get; set; }
    public int Quantita { get; set; }
    public Ordine Ordine { get; set; }
    
    public PortataOrdinata(Portata piatto, int quantita)
    {
        Piatto = piatto;
        Quantita = quantita;
    }
    public PortataOrdinata()        // Costruttore senza parametri
    {

    }
}