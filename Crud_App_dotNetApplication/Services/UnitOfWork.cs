using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Crud_App_dotNetApplication.Interfaces;
using Crud_App_dotNetApplication.Interfaces.IRepository;
using Crud_App_dotNetApplication.Repositories;
using Crud_App_dotNetCore.Entities;
using Crud_App_dotNetInfrastructure.Data;

namespace Crud_App_dotNetApplication.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IDbContextTransaction _transaction;

        private IGenericRepository<Category> _category;
        private IGenericRepository<Product> _product;
        public UnitOfWork(ApplicationDbContext context)
        {
            this._context = context;
        }
        public IGenericRepository<Product> Products => _product ??= new GenericRepository<Product>(_context);
        public IGenericRepository<Category> Categories => _category ??= new GenericRepository<Category>(_context);

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
