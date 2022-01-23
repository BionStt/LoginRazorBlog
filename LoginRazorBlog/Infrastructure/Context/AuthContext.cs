using LoginRazorBlog.Domain;
using LoginRazorBlog.Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginRazorBlog.Infrastructure.Context
{
    public class AuthContext : DbContext
    {
        public AuthContext()
        {

        }
        public AuthContext(DbContextOptions<AuthContext> options): base(options)
        {

        }
        public virtual DbSet<LocalAccountEntity> LocalAccount { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new LocalAccountConfiguration());


        }

    }
}
