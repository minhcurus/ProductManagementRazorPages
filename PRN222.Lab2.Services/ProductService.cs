using Microsoft.EntityFrameworkCore;
using PRN222.Lab2.Repositories.Interfaces;
using PRN222.Lab2.Repositories.Models;
using PRN222.Lab2.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN222.Lab2.Services
{
    public class ProductService :IProductService
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
            return await _context.Products.Include(p=>p.Category).ToListAsync();
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
    }
}
