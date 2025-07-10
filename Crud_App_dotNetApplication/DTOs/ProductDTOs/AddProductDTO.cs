using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crud_App_dotNetCore.Entities;

namespace Crud_App_dotNetApplication.DTOs.ProductDTOs
{
    public class AddProductDTO
    {
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public string SKU { get; set; }
        public string ImageUrl { get; set; }
        public bool IsActive { get; set; }
        public int CategoryId { get; set; }
    }
}
