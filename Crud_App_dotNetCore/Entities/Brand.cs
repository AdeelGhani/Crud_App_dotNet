namespace Crud_App_dotNetCore.Entities
{
    public class Brand : BaseEntity
    {
        public string BrandName { get; set; }
        public string Discription { get; set; }
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
