using Paradigmi.Application.Abstractions.Services;
using Paradigmi.Models.Entities;
using Paradigmi.Models.Repositories;

namespace Paradigmi.Application.Services
{
    public class OrdineService : IOrdineService
    {
        private readonly OrdineRepository _ordineRepository;

        public OrdineService(OrdineRepository ordineRepository)
        {
            _ordineRepository = ordineRepository;
        }

        public List<Ordine> GetOrdini(int from, int num, DateTime dataInizio, DateTime? dataFine, out int totalNum, Utente? utente)
        {
            return _ordineRepository.GetOrdini(from, num, dataInizio, dataFine, out totalNum, utente);
        }

        public Ordine? GetOrdine(int idOrdine)
        {
            return _ordineRepository.Ottieni(idOrdine);
        }

        public int AddOrdine(Utente utente, List<PortataOrdinata> portateOrdinate, out double costoTotale)
        {
            var ordine = new Ordine
            {
                Utente = utente,
                DataOrdine = DateTime.Now,
                PortateSelezionate = portateOrdinate
            };

            _ordineRepository.Aggiungi(ordine);
            _ordineRepository.Save();

            Dictionary<Tipologia, List<Portata>> elencoPortate = new Dictionary<Tipologia, List<Portata>>();

            foreach (var portataOrdinata in portateOrdinate)
            {
                if (!elencoPortate.ContainsKey(portataOrdinata.Portata.Tipo))
                {
                    elencoPortate[portataOrdinata.Portata.Tipo] = new List<Portata>();
                }

                for (int i = 0; i < portataOrdinata.Quantita; i++)
                {
                    elencoPortate[portataOrdinata.Portata.Tipo].Add(portataOrdinata.Portata);
                }
            }

            int nPastiCompleti = elencoPortate.Values.Min(p => p.Count);

            double costo = 0;
            foreach (var portate in elencoPortate.Values)
            {
                costo += ScontaCategoria(portate, nPastiCompleti);
            }
            
            costoTotale =  Math.Round(costo, 2); // Limita il risultato a due cifre decimali

            return ordine.NumeroOrdine;
        }

        private double ScontaCategoria(List<Portata> portate, int amount)
        {
            double costo = portate
                .OrderByDescending(p => p.Prezzo)
                .Select((p, index) => index >= amount ? p.Prezzo : p.Prezzo * 0.9) // Applica lo sconto del 10% se indice < amount
                .Sum();
            return costo;
        }
    }
}
