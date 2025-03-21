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
            int pageNumber,
            int pageSize,
            string sortOrder,
            bool ascending,
            string searchString)
        {
            Expression<Func<Product, bool>> filter = null;
            if (!string.IsNullOrEmpty(searchString))
            {
                filter = p => p.ProductName.Contains(searchString);
            }

            Expression<Func<Product, object>> orderBy = sortOrder switch
            {
                "ProductName" => p => p.ProductName,
                "UnitsInStock" => p => p.UnitsInStock,
                "UnitPrice" => p => p.UnitPrice,
                _ => p => p.ProductId
            };

            return await _unitOfWork.Products.GetPagedAsync(
                pageNumber,
                pageSize,
                filter,
                orderBy,
                ascending
            );
        }
    }
}
