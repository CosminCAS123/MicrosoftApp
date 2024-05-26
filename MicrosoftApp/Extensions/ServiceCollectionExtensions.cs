using Microsoft.Extensions.DependencyInjection;
using MicrosoftApp.Data;
using MicrosoftApp.Repositories;
using MicrosoftApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosoftApp.Extensions
{
    public  static class ServiceCollectionExtensions
    {
        public static void AddCommonServices(this IServiceCollection collection)
        {
            collection.AddDbContext<DatabaseContext>();

            collection.AddScoped<IUserRepository, UserRepository>();

            collection.AddScoped<IUserService, UserService>();
        }
    }
}
