namespace Paradigmi.Models.Entities;

public class PortataOrdinata
{
    public int OrdinazioneId { get; set; }

    public string PortataNome { get; set; } = String.Empty;
    public int Quantita { get; set; } = 1;

    public int Turno { get; set; } = 1;

    public Ordine Ordine { get; set; } = null!;
    public Portata Portata { get; set; } = null!;


    public PortataOrdinata(string portataNome, int quantita, int turno)
    {
        PortataNome = portataNome;
        Quantita = quantita;
        Turno = turno;
    }

    public PortataOrdinata()
    {
    }
}