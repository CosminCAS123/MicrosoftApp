using MicrosoftApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosoftApp.Services
{
    public interface IUserService
    {
        Task<bool> ExistsByUsernameAsync(string username);
        Task<bool> ExistsByEmailAdressAsync(string adress);
      
        Task AddUserAsync(User user);
        Task DeleteUserAsync(User user);
        Task<User> GetUserByIdAsync(int id);
        Task<List<User>> GetAllUsersAsync();
        Task<User> GetUserByUsername(string username);



    }
}
