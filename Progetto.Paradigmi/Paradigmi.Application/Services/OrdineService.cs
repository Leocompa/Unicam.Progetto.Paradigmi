using Paradigmi.Application.Abstractions.Services;
using Paradigmi.Models.Entities;
using Paradigmi.Models.Repositories;

namespace Paradigmi.Application.Services
{
    public class OrdineService : IOrdineService
    {
        private readonly OrdineRepository _ordineRepository;
        
        //TODO file config
        private readonly HashSet<Tipologia> _keyScontabili = new HashSet<Tipologia>() { Tipologia.Antipasto, Tipologia.Primo,Tipologia.Secondo,Tipologia.Contorno,Tipologia.Dolce };

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

            int nPastiCompleti = elencoPortate
                .Where(kv => _keyScontabili.Contains(kv.Key)) 
                .Select(kv => kv.Value.Count) 
                .Min();

            double costo = 0;
            foreach (var portate in elencoPortate.Values)
            {
                costo += ScontaCategoria(portate, nPastiCompleti);
            }
            
            costoTotale =  Math.Round(costo, 2); 

            return ordine.NumeroOrdine;
        }

        private double ScontaCategoria(List<Portata> portate, int amount)
        {
            var costoTotale = portate
                .OrderByDescending(p => p.Prezzo)
                .Select((p, index) => new { Portata = p, IsScontabile = index < amount && _keyScontabili.Contains(p.Tipo) })
                .Sum(p => p.IsScontabile ? p.Portata.Prezzo * 0.9 : p.Portata.Prezzo);
            
            return costoTotale;
        }
    }
}
