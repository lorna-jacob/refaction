using Refaction.Core.Models;
using System.Data.Entity;

namespace Refaction.Persistence
{
    public interface IApplicationDbContext
    {
        DbSet<Product> Products { get; set; }
        DbSet<ProductOption> ProductOptions { get; set; }
    }
}
