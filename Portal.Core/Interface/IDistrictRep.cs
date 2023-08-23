using Portal.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Core.Interface
{
    public interface IDistrictRep
    {
        Task<List<District>> GetAllAsync(Expression<Func<District, bool>> filter);
        Task<District> GetByIdAsync(Expression<Func<District, bool>> filter);
    }
}
