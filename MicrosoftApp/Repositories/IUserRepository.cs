using MicrosoftApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosoftApp.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<bool> ExistsWithUsernameAsync(string username);
        Task<bool> ExistsWithEmailAdressAsync(string adress);
        Task<User> GetByUsernameAsync(string username);
    }
}
