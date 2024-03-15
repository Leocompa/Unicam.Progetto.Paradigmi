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
    public List<Ordine> GetOrdini(int from, int num, DateTime dataInizio, DateTime dataFine, out int totalNum,
        Utente utente)
    {
        if (utente.Ruolo.Equals(Ruolo.Cliente))
        {
            var query = _context.Ordini.AsQueryable();
            query = query.Where(w => w.Utente.Equals(utente)).Where(or =>
                or.DataOrdine.CompareTo(dataInizio) >= 0 && or.DataOrdine.CompareTo(dataFine) <= 0);
            totalNum = query.Count();
            return query.OrderBy(ordine => ordine.NOrdine).Skip(from).Take(num).ToList();
        }
        else
        {
            var query = _context.Ordini.AsQueryable();
            query = query.Where(
                or => or.DataOrdine.CompareTo(dataInizio) >= 0 && or.DataOrdine.CompareTo(dataFine) <= 0);
            totalNum = query.Count();
            return query.OrderBy(ordine => ordine.NOrdine).Skip(from).Take(num).ToList();

        }
    }

    /**
     * getOrdine con quell'id
     */
    public List<Ordine> GetOrdine(int from, int num, string? filter, out int totalNum)
    {
        var query = _context.Ordini.AsQueryable();
        if (!string.IsNullOrEmpty(filter)) query = query.Where(w => w.NOrdine.Equals(filter));
        totalNum = query.Count();
        return query.OrderBy(ordine => ordine.NOrdine).Skip(from).Take(num).ToList();

    }
}