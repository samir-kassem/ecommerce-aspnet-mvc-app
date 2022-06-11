using tickets.Data.Base;
using tickets.Models;

namespace tickets.Data.Services
{
    public class ProducersService: EntityBaseRepository<Producer>, IProducersService
    {
        public ProducersService(AppDbContext db) : base(db){}
    }
}
