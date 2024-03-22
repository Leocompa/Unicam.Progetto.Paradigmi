using Microsoft.Extensions.Configuration;
using Paradigmi.Application.Abstractions.Services;
using Paradigmi.Models.Entities;
using Paradigmi.Models.Repositories;

namespace Paradigmi.Application.Services
{
    public class OrdineService : IOrdineService
    {
        private readonly OrdineRepository _ordineRepository;
        private readonly PortataRepository _portataRepository;
        private readonly HashSet<Tipologia> _keyScontabili;


        public OrdineService(OrdineRepository ordineRepository, PortataRepository portataRepository,
            IConfiguration configuration)
        {
            _ordineRepository = ordineRepository;
            _portataRepository = portataRepository;
            var keyScontabiliConfig = configuration.GetSection("PastoCompleto").Get<List<Tipologia>>();
            if (keyScontabiliConfig == null || !keyScontabiliConfig.Any())
            {
                _keyScontabili = new HashSet<Tipologia>
                {
                    Tipologia.Antipasto,
                    Tipologia.Primo,
                    Tipologia.Secondo,
                    Tipologia.Contorno,
                    Tipologia.Dolce
                };
            }
            else
            {
                _keyScontabili = new HashSet<Tipologia>(keyScontabiliConfig);
            }
        }

        public Ordine? GetOrdine(int idOrdine)
        {
            return _ordineRepository.Ottieni(idOrdine);
        }

        public int AddOrdine(string emailUtente, List<PortataOrdinata> portateOrdinate, Address? address,
            out decimal costoTotaleScontato, out decimal costoTotale, out int pastoCompleto)
        {
            var ordine = new Ordine
            {
                ClienteEmail = emailUtente,
                DataOrdine = DateOnly.FromDateTime(DateTime.Now),
                PortateSelezionate = portateOrdinate,
                IndirizzoConsegna = address
            };

            _ordineRepository.Aggiungi(ordine);
            _ordineRepository.Save();

            Dictionary<Tipologia, List<Portata>> elencoPortate = new Dictionary<Tipologia, List<Portata>>();

            foreach (var portataOrdinata in portateOrdinate)
            {
                Portata? portata = _portataRepository.Ottieni(portataOrdinata.PortataNome);
                if (portata == null)
                {
                    throw new Exception("portata non valida");
                }

                if (!elencoPortate.ContainsKey(portata.Tipo))
                {
                    elencoPortate[portata.Tipo] = new List<Portata>();
                }

                for (int i = 0; i < portataOrdinata.Quantita; i++)
                {
                    elencoPortate[portata.Tipo].Add(portataOrdinata.Portata);
                }
            }

            int nPastiCompleti = elencoPortate
                .Where(kv => _keyScontabili.Contains(kv.Key))
                .Select(kv => kv.Value.Count)
                .Min();

            decimal costo = 0;
            decimal costoTot = 0;
            foreach (var portate in elencoPortate.Values)
            {
                if (portate != null)
                {
                    costo += ScontaCategoria(portate, nPastiCompleti);
                    costoTot += GetCostoTotale(portate);
                }
            }

            costoTotaleScontato = costo;
            costoTotaleScontato = Math.Round(costo, 2);
            costoTotale = costoTot;
            pastoCompleto = nPastiCompleti;

            return ordine.NumeroOrdine;
        }

        private decimal GetCostoTotale(List<Portata> portate)
        {
            decimal costoTotale = 0;
            foreach (var portata in portate)
            {
                costoTotale += portata.Prezzo;
            }

            return costoTotale;
        }

        private decimal ScontaCategoria(List<Portata> portate, int amount)
        {
            if (portate.Count == 0)
            {
                return 0;
            }

            var costoTotale = portate
                .OrderByDescending(p => p.Prezzo)
                .Select((p, index) => new
                    { Portata = p, IsScontabile = index < amount && _keyScontabili.Contains(p.Tipo) })
                .Sum(p => p.IsScontabile ? p.Portata.Prezzo * 0.9M : p.Portata.Prezzo);

            return costoTotale;
        }

        public List<Ordine> GetStoricoOrdini(int from, int num, Ruolo ruolo, DateOnly? dataInizio, DateOnly? dataFine,
            string? email, out int totalNum)
        {
            if (dataInizio == null)
                dataInizio = DateOnly.MinValue;
            if (dataFine == null)
                dataFine = DateOnly.FromDateTime(DateTime.Now);

            switch (ruolo)
            {
                case Ruolo.Amministratore:
                    return _ordineRepository.GetOrdiniAmministratore(from, num, (DateOnly)dataInizio,
                        (DateOnly)dataFine, out totalNum,
                        email);
                case Ruolo.Cliente:
                    if (email == null)
                    {
                        throw new ArgumentException("parametro email non valido");
                    }

                    return _ordineRepository.GetOrdiniCliente(from, num, (DateOnly)dataInizio, (DateOnly)dataFine,
                        out totalNum, email);
                default:
                    throw new ArgumentException("ruolo non valido");
            }
        }
    }
}