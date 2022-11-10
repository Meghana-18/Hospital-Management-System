using System;
using Application.Entities.Base;

namespace Application.Dal.Contract
{
    public interface IEntityDataAccess<TEntity, in TPk> where TEntity : Entity
    {
        IEnumerable<TEntity> Get();
        TEntity Get(TPk id);
    }
}

