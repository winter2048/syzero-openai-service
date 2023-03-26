using System;
using System.Collections.Generic;
using System.Text;
using SyZero.Domain.Entities;
using SyZero.SqlSugar.Repositories;

namespace SyZero.OpenAI.Repository
{
    public class RepositoryBase<TEntity> : SqlSugarRepository<TEntity>
       where TEntity : class, IEntity, new()
    {
        public RepositoryBase(DbContext dbContext) : base(dbContext)
        {

        }
    }

}

