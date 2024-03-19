using Paradigmi.Application.Abstractions.Services;
using Paradigmi.Models.Entities;
using Paradigmi.Models.Repositories;

namespace Paradigmi.Application.Services;

public class OrdineService : IOrdineService
{
    private readonly OrdineRepository _ordineRepository;

    public OrdineService(OrdineRepository ordineRepository)
    {
        _ordineRepository = ordineRepository;
    }

    public List<Ordine> GetOrdini(int from, int num, DateTime dataInizio, DateTime? dataFine, out int totalNum,
        Utente? utente)
    {
        return _ordineRepository.GetOrdini(from, num, dataInizio, dataFine, out totalNum, utente);
    }


    public Ordine? GetOrdine(int idOrdine)
    {
        return _ordineRepository.Ottieni(idOrdine);
    }

    public int AddOrdine(Utente utente, List<PortataOrdinata> portateOrdinate, out double costoTotale)
    {
        var ordine = new Ordine();
        ordine.Utente = utente;
        ordine.DataOrdine = DateTime.Now;
        ordine.PortateSelezionate = portateOrdinate;

        _ordineRepository.Aggiungi(ordine);
        _ordineRepository.Save();

        VerificaPastoCompleto(portateOrdinate, out costoTotale);
        return ordine.NumeroOrdine;
    }


    private void VerificaPastoCompleto(List<PortataOrdinata> portateOrdinate, out double costoTotaleScontato)
    {
        bool pastoCompleto = false;
        costoTotaleScontato = 0;

        Dictionary<Tipologia, PortataOrdinata> pastoCompletoMaxPrezzo = new Dictionary<Tipologia, PortataOrdinata>();
        Dictionary<Tipologia, int> tipologieOrdinate = new Dictionary<Tipologia, int>();

        // Conta le portate per tipologia e verifica la portata con prezzo maggiore per tipologia
        foreach (var portataOrdinata in portateOrdinate)
        {
            if (!tipologieOrdinate.ContainsKey(portataOrdinata.Portata.Tipo))
            {
                tipologieOrdinate.Add(portataOrdinata.Portata.Tipo, 0);
            }

            tipologieOrdinate[portataOrdinata.Portata.Tipo] += portataOrdinata.Quantita;

            if (!pastoCompletoMaxPrezzo.ContainsKey(portataOrdinata.Portata.Tipo) ||
                portataOrdinata.Portata.Prezzo > pastoCompletoMaxPrezzo[portataOrdinata.Portata.Tipo].Portata.Prezzo)
            {
                pastoCompletoMaxPrezzo[portataOrdinata.Portata.Tipo] = portataOrdinata;
            }
        }

        // Verifica se sono presenti tutte le tipologie di portate per un pasto completo e se sono composte da una sola quantità
        if (tipologieOrdinate.ContainsKey(Tipologia.Antipasto) && tipologieOrdinate[Tipologia.Antipasto] >= 1 &&
            tipologieOrdinate.ContainsKey(Tipologia.Primo) && tipologieOrdinate[Tipologia.Primo] >= 1 &&
            tipologieOrdinate.ContainsKey(Tipologia.Secondo) && tipologieOrdinate[Tipologia.Secondo] >= 1 &&
            tipologieOrdinate.ContainsKey(Tipologia.Contorno) && tipologieOrdinate[Tipologia.Contorno] >= 1 &&
            tipologieOrdinate.ContainsKey(Tipologia.Dolce) && tipologieOrdinate[Tipologia.Dolce] >= 1)
        {
            pastoCompleto = true;

            // Calcola il totale dell'ordine con lo sconto del 10% solo per le portate del pasto completo
            foreach (var portataOrdinata in pastoCompletoMaxPrezzo.Values)
            {
                double prezzoPortataScontato = portataOrdinata.Portata.Prezzo - (portataOrdinata.Portata.Prezzo * 0.1);
                costoTotaleScontato += prezzoPortataScontato * portataOrdinata.Quantita;
            }
        }
        else
        {
            // Se il pasto non è completo, calcola il totale dell'ordine senza sconto per tutte le portate
            foreach (var portataOrdinata in portateOrdinate)
            {
                costoTotaleScontato += portataOrdinata.Portata.Prezzo * portataOrdinata.Quantita;
            }
        }
    }
}