using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud_App_dotNetApplication.DTOs.CategoryDTOs
{
    public class UpdateCategoryDTO
    {
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
        public string ImageUrl { get; set; }
        public bool IsActive { get; set; }

    }
}
