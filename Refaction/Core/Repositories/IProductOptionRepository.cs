using Refaction.Core.Models;
using System;
using System.Collections.Generic;

namespace Refaction.Core.Repositories
{
    public interface IProductOptionRepository : IRepository<ProductOption>
    {
        IEnumerable<ProductOption> GetAvailableOptionsForProduct(Guid productId);        
        ProductOption Get(Guid id, Guid productId);
        bool? IsExisting(Guid id, Guid productId);
    }
}
