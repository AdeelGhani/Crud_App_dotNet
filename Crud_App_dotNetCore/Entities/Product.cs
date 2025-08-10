using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud_App_dotNetCore.Entities
{
    public class Product: BaseEntity
    {
        public string ProductName { get; set; } = string.Empty;
        public string? ProductDescription { get; set; }
        public decimal? Price { get; set; }
        public int? StockQuantity { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; } = null!;

        // Brand relationship (many products to one brand). Make optional to ease migration
        public int? BrandId { get; set; }
        [ForeignKey("BrandId")]
        public Brand? Brand { get; set; }
    }
}
