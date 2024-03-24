using Microsoft.EntityFrameworkCore;
using Paradigmi.Models.Context;
using Paradigmi.Models.Entities;

namespace Paradigmi.Models.Repositories;

public class PortateOrdinateRepository : GenericRepository<PortataOrdinata>
{
    public PortateOrdinateRepository(MyDbContext context, PortataRepository portataRepository) : base(context)
    {
    }

    public List<PortataOrdinata> GetPortateOrdinate(int idOrdine)
    {
        var query = _context.PortateOrdinate.AsQueryable();
        query = query.Where(portata => portata.OrdinazioneId == idOrdine);
        return query.OrderBy(ordinata => ordinata.Turno).ToList();
    }

    public decimal GetCosto(int idOrdine, string portataNome)
    {
        decimal costoTotale = _context.PortateOrdinate
            .Where(portata => portata.PortataNome.Equals(portataNome) && portata.OrdinazioneId == idOrdine)
            .Include(portata => portata.Portata)
            .Sum(ordine => ordine.Quantita * ordine.Portata.Prezzo);

        return costoTotale;
    }
}