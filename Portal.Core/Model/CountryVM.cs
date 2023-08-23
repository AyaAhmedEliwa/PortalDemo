using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Core.Model
{
    public class CountryVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "*Department Name is required")]
        [MinLength(2, ErrorMessage = "*Min Length is 2")]
        [MaxLength(50, ErrorMessage = "*Max Length is 50")]
        public string Name { get; set; }
    }
}
