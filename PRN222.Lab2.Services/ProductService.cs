using Microsoft.EntityFrameworkCore;
using PRN222.Lab2.Repositories.Interfaces;
using PRN222.Lab2.Repositories.Models;
using PRN222.Lab2.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PRN222.Lab2.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly MyStoreContext _context;
        public ProductService(IUnitOfWork unitOfWork, MyStoreContext context)
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }
        public async Task<List<Product>> GetAllProduct()
        {
            //return await _unitOfWork.Products.GetAllAsync();
            return await _context.Products.Include(p => p.Category).ToListAsync();
        }

        public async Task<Product> GetProductById(int id)
        {
            return await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.ProductId == id);
        }
        public async Task AddProduct(Product product)
        {
            await _unitOfWork.Products.AddAsync(product);

        }
        public async Task UpdateProduct(Product product)
        {
            await _unitOfWork.Products.UpdateAsync(product);

        }
        public async Task DeleteProduct(Product product)
        {
            await _unitOfWork.Products.DeleteAsync(product);
        }

        public async Task<(List<Product> Items, int TotalCount)> GetPagedAsync(
                int pageNumber = 1, 
                int pageSize = 10, 
                string sortOrder = null, 
                bool ascending = true, 
                string searchString = null)
        {
            var query =  _context.Products.Include(p => p.Category) .AsQueryable();

            // Seartch
            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(p => p.ProductName.Contains(searchString));
            }

            // Sort
            if (!string.IsNullOrEmpty(sortOrder))
            {
                switch (sortOrder)
                {
                    case "ProductName":
                        query = ascending ? query.OrderBy(p => p.ProductName) : query.OrderByDescending(p => p.ProductName);
                        break;
                    case "UnitsInStock":
                        query = ascending ? query.OrderBy(p => p.UnitsInStock) : query.OrderByDescending(p => p.UnitsInStock);
                        break;
                    case "UnitPrice":
                        query = ascending ? query.OrderBy(p => p.UnitPrice) : query.OrderByDescending(p => p.UnitPrice);
                        break;
                    default:
                        query = query.OrderBy(p => p.ProductId); 
                        break;
                }
            }
            else
            {
                query = query.OrderBy(p => p.ProductId); 
            }

            var totalCount = await query.CountAsync();

            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, totalCount);
        }
    }
}
