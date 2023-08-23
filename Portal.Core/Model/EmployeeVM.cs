using Microsoft.AspNetCore.Http;
using Portal.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Core.Model
{
    public class EmployeeVM
    {
        public EmployeeVM()
        {
            IsActive = true;
            IsDeleted = false;
            IsUpdated = false;
            CreatedOn = DateTime.Now; 
            Id = Guid.NewGuid();
            Name = "";
            CVName = "";
            ImageName = "";
        }
        public Guid Id { get; set; }

        //[StringLength(50)]
        //[MinLength(3, ErrorMessage ="")]
        //[MaxLength(50, ErrorMessage = "")]
        public string Name { get; set; }

        //[Range(1000, 100000, ErrorMessage = "")]
        public double Salary { get; set; }

        //[EmailAddress(ErrorMessage ="")]
        public string? Email { get; set; }

        //[RegularExpression("[0-9]{2,5}-[a-zA-Z]{1,10}-[a-zA-Z]{1,10}", ErrorMessage = "12-street-city-country")]
        public string? Address { get; set; }
        public string? Notes { get; set; }

        public DateTime HireDate { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }

        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsUpdated { get; set; }

        public int DepartmentId { get; set; }
        public string? DepartmentName { get; set; } //if i do that mandatory ..
        public string? DepartmentCode { get; set; } // make mapper in Domainprofile

        public int? DistrictId { get; set; }
        public District? District { get; set; }
        public string? ImageName { get; set; }
        public string? CVName { get; set; }

        public IFormFile? Image { get; set; }    //binding to take file and
        public IFormFile? CV { get; set; }       //save it in server (used in view asp-for="")

        //[MinLength(6, ErrorMessage = "")]
        //[MaxLength(10, ErrorMessage = "")]
        //public string Password { get; set; }

        //[MinLength(6, ErrorMessage = "")]
        //[MaxLength(10, ErrorMessage = "")]
        //[Compare("Password" , ErrorMessage = "Password not match")]
        //public string ConfirmPassword { get; set; }
    }
}
