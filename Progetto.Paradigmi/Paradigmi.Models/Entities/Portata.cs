namespace Paradigmi.Models.Entities;

public class Portata
{
    
    public string Nome { get; set; } = String.Empty;
    public double Prezzo { get; set; } 
    public Tipologia Tipo { get; set; }


    public Portata(String nome, double prezzo, Tipologia tipo)
    {
        //TODO Rimuovere dopo test la data
        Nome = nome + DateTime.Now;
        Prezzo = prezzo;
        Tipo = tipo;
    }
}