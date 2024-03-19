using Paradigmi.Models.Entities;

namespace Paradigmi.Application.Models.Dtos;

public class OrdineDto
{
        
    public string ClienteEmail { get; set; } = String.Empty;
    public DateTime DataOrdine { get; set; }
    public int NumeroOrdine { get; set; }
    public Address IndirizzoConsegna { get; set; } = null!;

    public OrdineDto()
    {
        
    }

    public OrdineDto(Ordine ordine)
    {
        ordine.ClienteEmail = ClienteEmail;
        ordine.DataOrdine = DataOrdine;
        ordine.NumeroOrdine = NumeroOrdine;
        ordine.IndirizzoConsegna = IndirizzoConsegna;

    }
}