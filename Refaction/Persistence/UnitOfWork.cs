using Refaction.Core;
using Refaction.Core.Repositories;
using Refaction.Persistence.Repositories;
using System;

namespace Refaction.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _context = new ApplicationDbContext();

        private ProductOptionRepository _productOptionRepository;
        private ProductRepository _productRepository;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            _productOptionRepository = new ProductOptionRepository(context);
            _productRepository = new ProductRepository(context);            
        }

        public IProductOptionRepository ProductOptions
        {
            get
            {

                if (this._productOptionRepository == null)
                {
                    this._productOptionRepository = new ProductOptionRepository(_context);
                }
                return _productOptionRepository;
            }
        }

        public IProductRepository Products
        {
            get
            {

                if (this._productRepository == null)
                {
                    this._productRepository = new ProductRepository(_context);
                }
                return _productRepository;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }       
    }
}