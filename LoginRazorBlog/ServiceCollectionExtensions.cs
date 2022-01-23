using LoginRazorBlog.Infrastructure.Context;
using LoginRazorBlog.ServiceApplication;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LoginRazorBlog
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAuthStorage(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<AuthContext>(options =>
             options.UseSqlServer(connectionString, sqlOptions =>
             {
                 sqlOptions.EnableRetryOnFailure(
                     3,
                     TimeSpan.FromSeconds(300),
                     null);
             }));
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddScoped<ILocalAccountService, LocalAccountService>();

            return services;
        }
        
    }
}
