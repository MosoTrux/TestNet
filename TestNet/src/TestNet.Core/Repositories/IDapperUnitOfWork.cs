using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNet.Core.Entities;

namespace TestNet.Core.Repositories
{
    public interface IDapperUnitOfWork
    {
        IRepository<Product> ProductRepository { get; }

        void Commit();
    }
}
