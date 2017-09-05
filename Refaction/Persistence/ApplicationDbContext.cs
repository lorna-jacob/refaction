using Refaction.Core.Models;
using System;
using System.Data.Entity;

namespace Refaction.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductOption> ProductOptions { get; set; }        
    }
}