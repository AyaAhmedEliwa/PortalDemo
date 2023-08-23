using Portal.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Core.Model
{
    public class CityVM
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string Name { get; set; }

        public int CountryId { get; set; }

        //navigation property to relate to entities
        public string CountryName { get; set; }
    }
}
