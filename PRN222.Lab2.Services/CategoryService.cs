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
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<List<Category>> GetAllCategory()
        {
            return await _unitOfWork.Categories.GetAllAsync();
        }

        public async Task<Category> GetCategoryById(int id)
        {
            return await _unitOfWork.Categories.GetByIdAsync(id);
        }
        public async Task AddCategory(Category category)
        {
            await _unitOfWork.Categories.AddAsync(category);
        }
        public async Task UpdateCategory(Category category)
        {
            await _unitOfWork.Categories.UpdateAsync(category);
        }
        public async Task DeleteCategory(Category category)
        {
            await _unitOfWork.Categories.DeleteAsync(category);
        }
    }
}
