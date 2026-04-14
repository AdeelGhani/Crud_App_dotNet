using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud_App_dotNetCore.Entities
{
    public class Marrige: BaseEntity
    {
        [Required]
        public string Status { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
        public ICollection<Employee> Employees { get; set; }

    }
}
