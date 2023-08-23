using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Infrastructure.Entities
{
    [Table("Employees")]
    public class Employee
    {
        [Key]
        public Guid Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public double Salary { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? Notes { get; set; }

        [Required]
        public DateTime HireDate { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }

        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsUpdated { get; set; }

        public int DepartmentId { get; set; }

        //navigation property to relate to entities
        public Department? Department { get; set; }

        public int? DistrictId { get; set; }

        //navigation property to relate to entities
        public District? District { get; set; }

        public string? ImageName { get; set; }
        public string? CVName { get; set; }
    }
}
