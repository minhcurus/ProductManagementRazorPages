using PRN222.Lab2.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN222.Lab2.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        IGenericRepository<AccountMember> Accounts { get; }
        IGenericRepository<Category> Categories { get; }
        IGenericRepository<Product> Products { get; }
        Task<int> CompleteAsync();
    }
}
