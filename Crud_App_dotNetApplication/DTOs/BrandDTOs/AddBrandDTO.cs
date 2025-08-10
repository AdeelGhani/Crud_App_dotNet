using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud_App_dotNetApplication.DTOs.BrandDTOs
{
    public class AddBrandDTO
    {
        [Required (ErrorMessage = "Brand Name is Required")]
        public string BrandName { get; set; }
        public string? Discription { get; set; }
    }
}
