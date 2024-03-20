using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;
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

    //TODO gestire casi nulli
    public List<Ordine> GetOrdiniCliente(int from, int num, DateTime? dataInizio, DateTime? dataFine, out int totalNum,
        string email)
    {
        var query = _context.Ordini.AsQueryable();
        
        query = query.Where(ordine => ordine.Utente.Email == email);

        if (dataInizio != null)
        {
            query = query.Where(or => or.DataOrdine >= dataInizio);
        }
        if (dataFine != null)
        {
            query = query.Where(or => or.DataOrdine <= dataFine);
        }

        totalNum = query.Count();

        var risultatiPaginati = query.OrderBy(ordine => ordine.NumeroOrdine).Skip(from).Take(num).ToList();

        return risultatiPaginati;
    }

    public List<Ordine> GetOrdiniAmministratore(int from, int num, DateTime? dataInizio, DateTime? dataFine,
        out int totalNum, string? email)
    {
        var query = _context.Ordini.AsQueryable();

        if (email != null)
        {
            query = query.Where(ordine => ordine.Utente.Email == email);
        }

        if (dataInizio != null)
        {
            Console.WriteLine("DataInizio: " + dataInizio);
            query = query.Where(or => or.DataOrdine >= dataInizio);
        }
        if (dataFine != null)
        {
            Console.WriteLine("DataFine: " + dataFine);
            query = query.Where(or => or.DataOrdine <= dataFine);
        }

        totalNum = query.Count();

        Console.WriteLine(query.ToQueryString());
        var risultatiPaginati = query.OrderBy(ordine => ordine.NumeroOrdine).Skip(from).Take(Math.Min(num,totalNum-from)).ToList();

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