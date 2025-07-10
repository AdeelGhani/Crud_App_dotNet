using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crud_App_dotNetApplication.Interfaces.IRepository;
using Crud_App_dotNetCore.Entities;

namespace Crud_App_dotNetApplication.Interfaces
{
    public interface IUnitOfWork 
    {
        IGenericRepository<Product> Products { get; }
        IGenericRepository<Category> Categories { get; }


        Task<int> SaveChangesAsync();
    }
}
