using System;
using HealthcareApp.Models;

namespace HealthcareApp.Repositories
{
    public interface IEntityRepository<TEntity, TPk> where TEntity : class
    {
        ResponseStatus<TEntity> GetRecords();
        ResponseStatus<TEntity> GetRecord(TPk id);
    }
}

