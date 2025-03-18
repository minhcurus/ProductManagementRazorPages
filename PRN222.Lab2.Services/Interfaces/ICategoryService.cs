using PRN222.Lab2.Repositories.Interfaces;
using PRN222.Lab2.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN222.Lab2.Services.Interfaces
{
    public interface ICategoryService
    {

        Task<List<Category>> GetAllCategory();


        Task<Category> GetCategoryById(int id);

        Task AddCategory(Category category);

        Task UpdateCategory(Category category);

        Task DeleteCategory(Category category);

    }
}
