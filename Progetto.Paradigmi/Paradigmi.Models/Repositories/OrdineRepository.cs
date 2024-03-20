using Paradigmi.Models.Context;
using Paradigmi.Models.Entities;

namespace Paradigmi.Models.Repositories;

public class OrdineRepository : GenericRepository<Ordine>
{
    public OrdineRepository(MyDbContext context) : base(context)
    {
    }

    /**
     * GetOrdini tutti
     */

    public List<Ordine> GetOrdiniCliente(int from, int num, DateTime dataInizio, DateTime dataFine, out int totalNum,
        string email)
    {
        var query = _context.Ordini.AsQueryable()
            .Where(ordine => ordine.Utente.Email == email && ordine.DataOrdine >= dataInizio && ordine.DataOrdine <= dataFine);
        
        totalNum = query.Count();

        var risultatiPaginati = query.OrderBy(ordine => ordine.NumeroOrdine).Skip(from).Take(Math.Min(num,totalNum-from)).ToList();
        
        return risultatiPaginati;
    }

    public List<Ordine> GetOrdiniAmministratore(int from, int num, DateTime dataInizio, DateTime dataFine, out int totalNum, string? email)
    {
        var query = _context.Ordini
            .Where(ordine => (email == null || ordine.Utente.Email == email) && ordine.DataOrdine >= dataInizio && ordine.DataOrdine <= dataFine);

        // Se hai bisogno del conteggio totale per altre logiche, lascia questa riga
        totalNum = query.Count();

        var risultatiPaginati = query
            .OrderBy(ordine => ordine.NumeroOrdine)
            .Skip(from)
            .Take(Math.Min(num,totalNum-from))
            .ToList();

        return risultatiPaginati;
    }

    /**
     * getOrdine con quell'id
     */
    public List<Ordine> GetOrdine(int from, int num, string? filter, out int totalNum)
    {
        var query = _context.Ordini.AsQueryable();
        if (!string.IsNullOrEmpty(filter)) query = query.Where(w => w.NumeroOrdine.Equals(filter));
        totalNum = query.Count();
        return query.OrderBy(ordine => ordine.NumeroOrdine).Skip(from).Take(num).ToList();
    }
}