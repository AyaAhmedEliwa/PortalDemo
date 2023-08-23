using Microsoft.EntityFrameworkCore;
using Portal.Core.Interface;
using Portal.Core.Model;
using Portal.Infrastructure.Database;
using Portal.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Core.Reposatory
{
    public class DepartmentRep : IDepartmentRep
    {
        private readonly ApplicationContext db;

        public DepartmentRep(ApplicationContext db)
        {
            this.db = db;
        }

        public async Task<List<Department>> GetAllAsync()
        {
            var data = await db.Departments.ToListAsync();

            return data;
        }

        public async Task<Department> GetByIdAsync(int id)
        {
            var data = await db.Departments.Where(a => a.Id ==id).SingleOrDefaultAsync();

            return data;
        }
        public async Task CreateAsync(Department obj)
        {            
            await db.Departments.AddAsync(obj);
            await db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Department obj)
        {
            #region all code
            //var oldData = await db.Departments.FindAsync(obj.Id);
            //if (oldData != null)
            //{
            //    oldData.Name = obj.Name;
            //    oldData.Code = obj.Code;
            //}
            //else
            //{
            //    throw new NullReferenceException("No Entered Data");
            //}  
            #endregion
            //instead of all code

            db.Entry(obj).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }

        public async Task DeleteAsync(Department obj)
        {
            var oldData = await db.Departments.FindAsync(obj.Id);
            db.Departments.Remove(oldData);
            await db.SaveChangesAsync();
        }
    }
}
