using Portal.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Core.Interface
{
    public interface ICityRep
    {
        Task<List<City>> GetAllAsync(Expression<Func<City, bool>> filter);
        Task<City> GetByIdAsync(Expression<Func<City, bool>> filter);
    }
}
