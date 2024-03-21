namespace Paradigmi.Models.Entities;

public class Portata
{
    
    public string Nome { get; set; } = String.Empty;
    public decimal Prezzo { get; set; } 
    public Tipologia Tipo { get; set; }


    public Portata(String nome, decimal prezzo, Tipologia tipo)
    {
        //TODO Rimuovere dopo test la data
        Nome = nome;
        Prezzo = prezzo;
        Tipo = tipo;
    }
    
}