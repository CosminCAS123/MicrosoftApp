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
        private readonly string ConnectionString = "Host=localhost;Port=5432;Username=postgres;Password=Teodora123;Database=OrganizeITdatabase";
        public DbSet<User> Users { get; set; }
        public DbSet<TaskList> TaskLists { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)    
        {
            optionsBuilder.UseNpgsql(this.ConnectionString);
        }

    }
}
