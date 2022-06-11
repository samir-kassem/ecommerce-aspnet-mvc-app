using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tickets.Data.Base;
using tickets.Models;

namespace tickets.Data.Services
{
    public class ActorsService : EntityBaseRepository<Actor>,IActorsService
    {
        public ActorsService(AppDbContext db) : base(db) { }
    
    }
}
