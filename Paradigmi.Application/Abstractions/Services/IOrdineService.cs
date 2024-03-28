using Paradigmi.Models.Entities;

namespace Paradigmi.Application.Abstractions.Services;

public interface IOrdineService
{
    List<Ordine> GetStoricoOrdini(int from, int num, Ruolo ruolo, DateOnly? dataInizio, DateOnly? dataFine,
        string? email,
        out int totalNum);

    int AddOrdine(string emailUtente, List<PortataOrdinata> portateOrdinate, Address? address,
        out decimal costoTotaleScontato, out decimal costoTotale, out int pastoCompleto);

    Ordine? GetOrdine(int idOrdine);
}