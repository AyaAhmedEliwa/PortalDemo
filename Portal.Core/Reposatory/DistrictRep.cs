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
    public class DistrictRep : IDistrictRep
    {
        private readonly ApplicationContext db;

        public DistrictRep(ApplicationContext db)
        {
            this.db = db;
        }
        public async Task<List<District>> GetAllAsync(Expression<Func<District, bool>> filter)
        {
            //Egger Load
            var data = await db.Districts.Include(d => d.City).Where(filter).ToListAsync();

            return data;
        }

        public async Task<District> GetByIdAsync(Expression<Func<District, bool>> filter)
        {
            var data = await db.Districts.Include(d => d.City).Where(filter).SingleOrDefaultAsync();

            return data;
        }
    }
}
