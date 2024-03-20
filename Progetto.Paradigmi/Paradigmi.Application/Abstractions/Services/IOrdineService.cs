using Paradigmi.Models.Entities;

namespace Paradigmi.Application.Abstractions.Services;

public interface IOrdineService
{
    List<Ordine> GetStoricoOrdini(int from, int num, Utente utente, DateTime? dataInizio, DateTime? dataFine,
        string? email,
        out int totalNum);

    int AddOrdine(Utente utente, List<PortataOrdinata> portateOrdinate, Address? address, out decimal costoTotale);
    Ordine? GetOrdine(int idOrdine);
}