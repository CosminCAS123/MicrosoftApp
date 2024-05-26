using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosoftApp.Repositories
{
    public interface IRepository<T> where T : class
    {
        //CRUD
        Task AddAsync(T entity);
        Task Delete(T entity);
        Task UpdateAsync();
        Task<T> GetByIdAsync(int id);
        Task<List<T>> GetAllAsync();

    }
}
