using Paradigmi.Models.Entities;

namespace Paradigmi.Application.Abstractions.Services;

public interface IOrdineService
{

    List<Ordine> GetOrdini(int from, int num, DateTime dataInizio, DateTime? dataFine, out int totalNum, Utente? utente);
    int AddOrdine(Utente utente, List<PortataOrdinata> portateOrdinate, out double costoTotale);
    Ordine? GetOrdine(int idOrdine);
}