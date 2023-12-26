using TestNet.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TestNet.Core.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        T GetById(long id);
        void Add(T entity, Dictionary<string, object> parameteres);
        void Update(Dictionary<string, object> parameteres);
    }
}
