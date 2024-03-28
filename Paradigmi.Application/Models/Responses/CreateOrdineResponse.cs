using Paradigmi.Application.Models.Responses;
using Paradigmi.Models.Entities;

namespace Paradigmi.Application.Responses;

public class CreateOrdineResponse
{
    public int NumeroOrdine { get; set; }
    public Address Indirizzo { get; set; }
    public decimal CostoTotale { get; set; }
    public decimal CostoTotaleScontato { get; set; }
    public List<string> PortateOrdinate { get; set; }
    public int NumeroPastiCompleti { get; set; }

    public CreateOrdineResponse(int numeroOrdine, Address indirizzo, decimal costoTotale, decimal costoTotaleScontato,
        int numeroPastiCompleti, List<CreatePortateOrdinateRigaResponse> portateOrdinate)
    {
        NumeroOrdine = numeroOrdine;
        Indirizzo = indirizzo;
        CostoTotale = costoTotale;
        CostoTotaleScontato = costoTotaleScontato;
        NumeroPastiCompleti = numeroPastiCompleti;
        PortateOrdinate = new List<string>();
        foreach (var portata in portateOrdinate)
        {
            PortateOrdinate.Add(portata.Riga);
        }
    }
}