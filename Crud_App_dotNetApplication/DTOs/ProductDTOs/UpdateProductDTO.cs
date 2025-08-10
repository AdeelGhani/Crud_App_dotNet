using System;

namespace Crud_App_dotNetApplication.DTOs.ProductDTOs
{
    public class UpdateProductDTO
    {
        public string ProductName { get; set; } = string.Empty;
        public string? ProductDescription { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public bool IsActive { get; set; }
        public int CategoryId { get; set; }
        public int BrandId { get; set; }
    }
}