using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Portal.Core.Model;
using Portal.Infrastructure.Entities;
using Portal.Infrastructure.Extend;

namespace Portal.Core.Mapper
{
    public class DomainProfile : Profile
    {

        public DomainProfile()
        {
            #region map Department
            CreateMap<Department, DepartmentVM>().ReverseMap();
            //map each one to each other
            #endregion

            #region map Employee
            CreateMap<EmployeeVM, Employee>().ReverseMap()
                //we don't need to map all of this as it related to employee

                //.ForPath(dest => dest.Id, source => source.MapFrom(a => a.Id))
                //.ForPath(dest => dest.Name, source => source.MapFrom(a => a.Name))
                //.ForPath(dest => dest.Salary, source => source.MapFrom(a => a.Salary))
                //.ForPath(dest => dest.Email, source => source.MapFrom(a => a.Email))
                //.ForPath(dest => dest.Address, source => source.MapFrom(a => a.Address))
                //.ForPath(dest => dest.HireDate, source => source.MapFrom(a => a.HireDate))
                //.ForPath(dest => dest.CreatedOn, source => source.MapFrom(a => a.CreatedOn))
                //.ForPath(dest => dest.DeletedOn, source => source.MapFrom(a => a.DeletedOn))
                //.ForPath(dest => dest.UpdatedOn, source => source.MapFrom(a => a.UpdatedOn))
                //.ForPath(dest => dest.IsUpdated, source => source.MapFrom(a => a.IsUpdated))
                //.ForPath(dest => dest.IsActive, source => source.MapFrom(a => a.IsActive))
                //.ForPath(dest => dest.IsDeleted, source => source.MapFrom(a => a.IsDeleted))
                //.ForPath(dest => dest.Notes, source => source.MapFrom(a => a.Notes))

                //but these data related to department (navigation property)
                .ForPath(dest => dest.DepartmentName, source => source.MapFrom(a => a.Department.Name))
                .ForPath(dest => dest.DepartmentCode, source => source.MapFrom(a => a.Department.Code));
            #endregion

            #region map Country
            CreateMap<Country, CountryVM>().ReverseMap();            

            CreateMap<CityVM, City>().ReverseMap();

            CreateMap<DistrictVM, District>().ReverseMap();

            #endregion

            #region map Register User
            CreateMap<RegistrationVM, ApplicationUser>().ReverseMap()
                                             .ForPath(dest => dest.UserName, source => source.MapFrom(a => a.UserName))
                                             .ForPath(dest => dest.Email, source => source.MapFrom(a => a.Email))
                                             .ForPath(dest => dest.IsAgree, source => source.MapFrom(a => a.IsAgree));
            #endregion
        }

    }
}
