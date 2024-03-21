using Paradigmi.Models.Context;
using Paradigmi.Models.Entities;

namespace Paradigmi.Models.Repositories;

public class PortataRepository : GenericRepository<Portata>
{
    public PortataRepository(MyDbContext context) : base(context)
    {
    }

    public List<Portata> GetPortateByTipo(int from, int num, Tipologia? tipologia, out int totalNum)
    {
        var query = _context.Portate.AsQueryable();
        if (tipologia != null) query = query.Where(w => w.Tipo.Equals(tipologia));
        totalNum = query.Count();
        return query.OrderBy(portata => portata.Nome).Skip(from).Take(num).ToList();
    }

    public List<Portata> GetPortateByName(int from, int num, string? filter, out int totalNum)
    {
        var query = _context.Portate.AsQueryable();
        if (!string.IsNullOrEmpty(filter)) query = query.Where(w => w.Nome.ToLower().Contains(filter.ToLower()));
        totalNum = query.Count();
        return query.OrderBy(portata => portata.Nome).Skip(from).Take(num).ToList();
    }

    public List<Portata> GetPortate(string? nome, Tipologia? tipologia)
    {
        var query = _context.Portate.AsQueryable();
        if (tipologia != null)
        {
            query = query.Where(portata => portata.Tipo == tipologia);
        }

        if (nome != null)
        {
            query = query.Where(portata => portata.Nome.ToLower().Contains(nome.ToLower()));
        }

        return query.OrderBy(portata => portata.Nome).ToList();
    }
}