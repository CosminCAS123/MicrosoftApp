using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosoftApp.Data
{
    public class DatabaseContext : DbContext
    {
        private readonly string ConnectionString = "notoriginalstring";
        public DbSet<User> Users { get; set; }
        public DbSet<TaskList> TaskLists { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)    
        {
            optionsBuilder.UseNpgsql(this.ConnectionString);
        }

    }
}
