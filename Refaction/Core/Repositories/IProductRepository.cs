using Refaction.Core.Models;
using System;
using System.Collections.Generic;

namespace Refaction.Core.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        IEnumerable<Product> GetAll();
        IEnumerable<Product> SearchByName(string name);
        Product Get(Guid id);
        bool IsExisting(Guid id);
    }
}
