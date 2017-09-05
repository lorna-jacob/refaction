using Refaction.Core.Models;
using Refaction.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Refaction.Core
{
    public interface IUnitOfWork
    {
        IProductRepository Products { get; }
        IProductOptionRepository ProductOptions { get; }
        void Save();
    }
}
