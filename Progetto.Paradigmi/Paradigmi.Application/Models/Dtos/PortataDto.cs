using Paradigmi.Models.Entities;

namespace Paradigmi.Application.Models.Dtos;

public class PortataDto
{
    public string Nome { get; set; } = String.Empty;
    public decimal Prezzo { get; set; } 
    public Tipologia Tipo { get; set; }


    public PortataDto(Portata portata)
    {
        
        Nome = portata.Nome;
        Prezzo = portata.Prezzo;
        Tipo = portata.Tipo;
    }
}