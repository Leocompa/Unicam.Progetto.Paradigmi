using Paradigmi.Models.Entities;

namespace Paradigmi.Application.Models.Dtos;

public class OrdineDto
{
        
    public string ClienteEmail { get; set; } = String.Empty;
    public DateOnly DataOrdine { get; set; }
    public int NumeroOrdine { get; set; }
    public Address IndirizzoConsegna { get; set; } = null!;

    public OrdineDto()
    {
        
    }

    public OrdineDto(Ordine ordine)
    {
        ClienteEmail= ordine.ClienteEmail;
        DataOrdine = ordine.DataOrdine;
        NumeroOrdine = ordine.NumeroOrdine;
        IndirizzoConsegna = ordine.IndirizzoConsegna;

    }
}