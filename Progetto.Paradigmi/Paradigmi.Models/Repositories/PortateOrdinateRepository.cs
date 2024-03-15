using Paradigmi.Models.Context;
using Paradigmi.Models.Entities;

namespace Paradigmi.Models.Repositories;

public class PortateOrdinateRepository : GenericRepository<PortataOrdinata>
{
    public PortateOrdinateRepository(MyDbContext context) : base(context)
    {
    }
}
    
