using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Papara.Infrastructure.DTO
{
    public class MemberDTO
    {
        [Required(ErrorMessage = "This information is required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "This information is required")]
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
