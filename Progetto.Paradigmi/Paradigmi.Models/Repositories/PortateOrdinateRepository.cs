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

    public decimal getCosto(int idOrdine,string portataNome)
    {
        decimal costoTotale = _context.PortateOrdinate
            .Where(portata => portata.PortataNome.Equals(portataNome) && portata.OrdinazioneId == idOrdine)
            .Include(portata => portata.Portata)
            .Sum(ordine => ordine.Quantita * ordine.Portata.Prezzo);
        
        return costoTotale;
    }
}
    
