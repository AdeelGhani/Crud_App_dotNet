using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud_App_dotNetApplication.DTOs.BrandDTOs
{
    public class FetchBrandDTO
    {
        public int Id { get; set; }
        public string BrandName { get; set; }
        public string? Discription { get; set; }
    }
}
