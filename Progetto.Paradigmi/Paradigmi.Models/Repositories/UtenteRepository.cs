using Paradigmi.Models.Context;
using Paradigmi.Models.Entities;

namespace Paradigmi.Models.Repositories;

public class UtenteRepository : GenericRepository<Utente>
{
    public UtenteRepository(MyDbContext context) : base(context)
    {
    }

    public List<Utente> GetUtenti(int from, int num, string? filter, out int totalNum)
    {
        var query = _context.Utenti.AsQueryable();
        if (!string.IsNullOrEmpty(filter)) query = query.Where(w => w.Email.ToLower().Contains(filter.ToLower()));
        totalNum = query.Count();
        return query.OrderBy(utente => utente.Email).Skip(from*num).Take(Math.Min(num,totalNum-from)).ToList();
    }

    public List<Utente> GetUtenti()
    {
        var query = _context.Utenti.AsQueryable();
        return query.OrderBy(utente => utente.Email).ToList();
    }
}
