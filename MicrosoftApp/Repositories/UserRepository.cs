using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MicrosoftApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosoftApp.Repositories
{
    public class UserRepository : IUserRepository
    {
        private DatabaseContext context = null!;
        public UserRepository(DatabaseContext dbcontext) => this.context = dbcontext; 
        
        public async Task AddAsync(User entity)
        {
            await this.context.Users.AddAsync(entity);
            await UpdateAsync();
        }

        public async Task Delete(User entity)
        {
            this.context.Users.Remove(entity);
             await  UpdateAsync();
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await this.context.Users.ToListAsync();
        }

        public async Task<User> GetByIdAsync(int id) => await this.context.Users.FirstOrDefaultAsync(x => x.ID == id);

        public async Task<User> GetByUsernameAsync(string username) => await context.Users.FirstOrDefaultAsync(x => string.Equals(x.Username, username));
       public async Task<bool> ExistsWithUsernameAsync(string username)
        {
            var user = await GetByUsernameAsync(username);
            return user is null ? false : true;
           
        }
        public async Task<bool> ExistsWithEmailAdressAsync(string adress)
        {
            var user = await this.context.Users.FirstOrDefaultAsync(x => string.Equals(x.Email, adress));
            return user is null ? false : true;
        }
       public async Task UpdateAsync()
        {
            await this.context.SaveChangesAsync();
        }
    }
}
