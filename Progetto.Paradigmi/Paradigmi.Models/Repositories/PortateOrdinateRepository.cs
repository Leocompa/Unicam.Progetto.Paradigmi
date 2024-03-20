using Microsoft.EntityFrameworkCore;
using Paradigmi.Models.Context;
using Paradigmi.Models.Entities;

namespace Paradigmi.Models.Repositories;

public class PortateOrdinateRepository : GenericRepository<PortataOrdinata>
{
    private readonly PortataRepository _portataRepository;
    public PortateOrdinateRepository(MyDbContext context,PortataRepository portataRepository) : base(context)
    {
        _portataRepository = portataRepository;
    }
    
    public List<PortataOrdinata> getPortateOrdinate(int idOrdine)
    {
        var query = _context.PortateOrdinate.AsQueryable();
        query=query.Where(portata => portata.OrdinazioneId == idOrdine);
        return query.ToList();
    }

    public double getCosto(int idOrdine,string portataNome)
    {
        var query = _context.PortateOrdinate.AsQueryable();
        query=query.Where(portata => portata.PortataNome.Equals(portataNome))
            .Where(portata => portata.OrdinazioneId == idOrdine)
            .Include(portata => portata.Portata);;
        Console.WriteLine(query.ToQueryString());
        
        Console.WriteLine(query.ToList()[0].Quantita+","+query.ToList()[0].Portata.Prezzo);
        
        
        double costoTotale = query
            .ToList()
            .Sum(ordine => ordine.Quantita * ordine.Portata.Prezzo);
        
        return costoTotale;
    }
}
    
