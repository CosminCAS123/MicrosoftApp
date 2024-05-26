using Microsoft.Extensions.DependencyInjection;
using MicrosoftApp.Data;
using MicrosoftApp.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosoftApp.Services
{
    public class UserService : IUserService
    {
        private IUserRepository repository;
        public UserService(IUserRepository userRepository) => this.repository = userRepository;
        

        public async Task<bool> ExistsByUsernameAsync(string username)
        {
            return await this.repository.ExistsWithUsernameAsync(username);
        }
        public async Task<bool> ExistsByEmailAdressAsync(string adress)
        {
            return await this.repository.ExistsWithEmailAdressAsync(adress);
        }

        public async Task AddUserAsync(User user)
        {
            await this.repository.AddAsync(user);
        }

        public async Task DeleteUserAsync(User user)
        {
            await this.repository.Delete(user);
        }
        public async Task<User> GetUserByIdAsync(int id)
        {
            return await this.repository.GetByIdAsync(id);
        }
        public async Task<User> GetUserByUsername(string username)
        {
            return await this.repository.GetByUsernameAsync(username);
        }
        public async Task<List<User>> GetAllUsersAsync()
        {
            return await this.repository.GetAllAsync();
        }

     
    }
}
