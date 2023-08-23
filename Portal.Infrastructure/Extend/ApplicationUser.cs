using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Infrastructure.Extend
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            CreateOn = DateTime.Now.ToShortDateString();
            IsActive = true;
            IsDeleted = false;
        }
        public string CreateOn { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        [Required(ErrorMessage = "you should agree about the roles")]
        public bool IsAgree { get; set; }

    }
}
