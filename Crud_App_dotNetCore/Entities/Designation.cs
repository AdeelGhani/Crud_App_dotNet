namespace Crud_App_dotNetCore.Entities
{
    public class Designation:BaseEntity
    {
        public string DesignationName { get; set; }
        public string Description { get; set; }
        public ICollection<Employee> Employees { get; set; }
    }
}
