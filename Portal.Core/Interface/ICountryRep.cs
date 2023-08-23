using Portal.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Core.Interface
{
    public interface ICountryRep
    {
        Task<List<Country>> GetAllAsync(Expression<Func<Country, bool>> filter = null);
        Task<Country> GetByIdAsync(Expression<Func<Country, bool>> filter = null);
    }
}
