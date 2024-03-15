using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Paradigmi.Models.Entities;

public class PortataOrdinata
{

    public int OrdinazioneId { get; set; } 

    public string PortataNome { get; set; } 
    public int Quantita { get; set; }
    
    public Ordine Ordine { get; set; }
    public Portata Portata { get; set; } 
}