using Portal.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Core.Interface
{
    public interface IEmployeeRep
    {
        Task<List<Employee>> GetAllAsync(Expression<Func<Employee,bool>> filter);
        Task<Employee> GetByIdAsync(Expression<Func<Employee, bool>> filter);
        Task CreateAsync(Employee obj);
        Task UpdateAsync(Employee obj);
        Task DeleteAsync(Employee obj);
    }
}
