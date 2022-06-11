using tickets.Data.Base;
using tickets.Models;

namespace tickets.Data.Services
{
    public class CinemasService:EntityBaseRepository<Cinema>, ICinemasService
    {
        public CinemasService(AppDbContext db):base(db){}
    }
}
