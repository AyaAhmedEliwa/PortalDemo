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
    public class CountryRep : ICountryRep
    {
        
        private readonly ApplicationContext db;

        public CountryRep(ApplicationContext db)
        {
            this.db = db;
        }
        public async Task<List<Country>> GetAllAsync(Expression<Func<Country, bool>> filter = null)
        {
            var data = await db.Countries.ToListAsync();

            return data;
        }

        public async Task<Country> GetByIdAsync(Expression<Func<Country, bool>> filter = null)
        {
            var data = await db.Countries.Where(filter).SingleOrDefaultAsync();

            if (data == null)
                throw new NullReferenceException("");

            return data;
        }
    }
}
