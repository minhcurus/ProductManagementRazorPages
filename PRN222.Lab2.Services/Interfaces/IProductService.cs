using PRN222.Lab2.Repositories.Interfaces;
using PRN222.Lab2.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PRN222.Lab2.Services.Interfaces
{
    public interface IProductService
    {

        Task<List<Product>> GetAllProduct();


        Task<Product> GetProductById(int id);

        Task AddProduct(Product product);

        Task UpdateProduct(Product product);

        Task DeleteProduct(Product product);

        Task<(List<Product> Items, int TotalCount)> GetPagedAsync(
        int pageNumber = 1,
        int pageSize = 10,
        string sortOrder = null,
        bool ascending = true,
        string searchString = null);

    }
}
