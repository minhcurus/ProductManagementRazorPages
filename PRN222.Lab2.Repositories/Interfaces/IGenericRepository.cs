﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN222.Lab2.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync() ;
        Task<T> GetByIdAsync(int id) ;
        Task<T> GetByNameAsync(string name);
        Task AddAsync(T entity) ;
        Task UpdateAsync(T entity) ;
        Task DeleteAsync(T entity) ;

    }
}
