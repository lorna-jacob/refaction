using Refaction.Core.Models;
using Refaction.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Refaction.Persistence.Repositories
{
    public class ProductOptionRepository : IProductOptionRepository
    {
        private ApplicationDbContext _context;

        public ProductOptionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<ProductOption> GetAvailableOptionsForProduct(Guid productId)
        {
            return _context.ProductOptions.Where(p => p.ProductId == productId);
        }

        public ProductOption Get(Guid id, Guid productId)
        {
            return _context.ProductOptions.SingleOrDefault(p => (p.ProductId == productId) && (p.Id == id));
        }

        public void Insert(ProductOption productOption)
        {
            _context.ProductOptions.Add(productOption);
        }

        public void Update(ProductOption productOption)
        {
            _context.Entry(productOption).State = EntityState.Modified;
        }

        public void Delete(object id)
        {            
            Guid guid = (Guid)id;
            ProductOption productOption = _context.ProductOptions.Find(id);
            if (productOption != null)
            {
                _context.ProductOptions.Remove(productOption);
            }
        }

        public bool? IsExisting(Guid id, Guid productId)
        {
            return _context?.ProductOptions
                .Any(p => p.Id == id && p.ProductId == productId);
        }
    }
}