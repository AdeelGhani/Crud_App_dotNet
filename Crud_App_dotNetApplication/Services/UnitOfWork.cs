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

        private IGenericRepository<JournalDetail> _journalDetail;
        //this is Brand Field
        private IGenericRepository<Journal> _journal;
        private IGenericRepository<User> _user;
        public UnitOfWork(ApplicationDbContext context)
        {
            this._context = context;
        }
         public IGenericRepository<JournalDetail> JournalDetails => _journalDetail ??= new GenericRepository<JournalDetail>(_context);
        public IGenericRepository<Journal> Journal => _journal ??= new GenericRepository<Journal>(_context);
        public IGenericRepository<User> Users => _user ??= new GenericRepository<User>(_context);
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
