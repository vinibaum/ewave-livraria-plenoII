using Livraria.Domain.Interfaces.Repositories.Base;
using Livraria.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Livraria.Infra.Data.Repositories.Base
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {

        protected readonly LivrariaContext _Context;
        protected DbSet<TEntity> _DbSet;

        public RepositoryBase(LivrariaContext context)
        {
            _Context = context;
            _DbSet = _Context.Set<TEntity>();
        }
        public async Task Save(TEntity entity)
        {
            _DbSet.Add(entity);
            await _Context.SaveChangesAsync();
        }

        public async Task Update(TEntity entity)
        {
            _DbSet.Update(entity);
            await _Context.SaveChangesAsync();
        }

        public async Task Delete(TEntity entity)
        {
            _DbSet.Remove(entity);
            await _Context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _Context.Dispose();
        }

    }
}
