using Refaction.Core.Repositories;
using System.Collections.Generic;
using Refaction.Core.Models;
using System;
using System.Linq;
using System.Data.Entity;
using Refaction.Core;

namespace Refaction.Persistence.Repositories
{
    public class ProductRepository : IProductRepository
    {        
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Product> GetAll()
        {
            return _context.Products.ToList();
        }

        public IEnumerable<Product> SearchByName(string name)
        {
            return _context.Products.Where(p => p.Name.Contains(name));
        }

        public Product Get(Guid id)
        {
            return _context.Products.Find(id);
        }

        public void Insert(Product product)
        {
            _context.Products.Add(product);
        }

        public void Update(Product product)
        {
            _context.Entry(product).State = EntityState.Modified;
        }

        public void Delete(object id)
        {
            Guid guid = (Guid)id;
            Product product = _context.Products.Find(id);
            if (product != null)
            {
                _context.Products.Remove(product);                
            }
        }

        public bool IsExisting(Guid id)
        {
            return _context.Products.Any(p => p.Id == id);
        }
    }
}