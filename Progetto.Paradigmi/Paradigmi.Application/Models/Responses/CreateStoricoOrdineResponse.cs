using Paradigmi.Models.Entities;

namespace Paradigmi.Application.Models.Responses;

public class CreateStoricoOrdineResponse
{
    public string ClienteEmail { get; set; }
    public DateOnly DataOrdine { get; set; }
    public int NumeroOrdine { get; set; }
    public Address? Indirizzo { get; set; }


    public CreateStoricoOrdineResponse(string clienteEmail, DateOnly dataOrdine, int numeroOrdine, Address? indirizzo)
    {
        ClienteEmail = clienteEmail;
        DataOrdine = dataOrdine;
        NumeroOrdine = numeroOrdine;
        Indirizzo = indirizzo;
    }
}