using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Paradigmi.Models.Entities;

public class Ordine
{
    public Utente Utente { get; set; } = null!;
    public DateTime DataOrdine { get; set; }
    
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long NOrdine { get; set; }
    public Address IndirizzoConsegna { get; set; } = null!;
    public Dictionary<int, Portata> Portate { get; set; } = null!;

    public Ordine(Utente utente, DateTime dataOrdine)
    {
        Utente = utente;
        DataOrdine = dataOrdine;

    }
}
