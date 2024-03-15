using System.ComponentModel.DataAnnotations;

namespace Paradigmi.Models.Entities;

public class Portata
{
    
    public string Nome { get; set; } = String.Empty;
    public double Prezzo { get; set; }
    public Tipologia Tipo { get; set; } 


}