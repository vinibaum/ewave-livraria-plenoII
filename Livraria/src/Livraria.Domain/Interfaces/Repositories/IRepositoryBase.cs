using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Livraria.Domain.Interfaces.Repositories.Base
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
         Task Save(TEntity entity);
         Task Update(TEntity entity);
         Task Delete(TEntity entity); 

    }
}
