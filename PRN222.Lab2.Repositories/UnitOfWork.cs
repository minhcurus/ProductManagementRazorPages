using Microsoft.EntityFrameworkCore;
using PRN222.Lab2.Repositories.Interfaces;
using PRN222.Lab2.Repositories.Models;
using System;
using System.Threading.Tasks;

namespace PRN222.Lab2.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly MyStoreContext _context;

        public IGenericRepository<AccountMember> Accounts { get; }
        public IGenericRepository<Category> Categories { get; }
        public IGenericRepository<Product> Products { get; }

        public UnitOfWork(MyStoreContext context,
                          IGenericRepository<AccountMember> accountRepository,
                          IGenericRepository<Category> categoryRepository,
                          IGenericRepository<Product> productRepository)
        {
            _context = context;
            Accounts = accountRepository;
            Categories = categoryRepository;
            Products = productRepository;
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}