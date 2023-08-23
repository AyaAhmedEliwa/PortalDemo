using Microsoft.EntityFrameworkCore;
using Portal.Core.Interface;
using Portal.Infrastructure.Database;
using Portal.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Core.Reposatory
{
    public class CityRep : ICityRep
    {
        private readonly ApplicationContext db;

        public CityRep(ApplicationContext db)
        {
            this.db = db;
        }
        public async Task<List<City>> GetAllAsync(Expression<Func<City, bool>> filter)
        {
            //Egger Load
            var data = await db.Cities.Include(c => c.Country).Where(filter).ToListAsync();

            return data;
        }

        public async Task<City> GetByIdAsync(Expression<Func<City, bool>> filter)
        {
            var data = await db.Cities.Include(c => c.Country).Where(filter).SingleOrDefaultAsync();
            if (data == null)
                throw new NullReferenceException("");

            return data;
        }
    }
}
