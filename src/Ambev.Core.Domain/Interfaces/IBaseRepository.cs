﻿using Ambev.Core.Domain.Common;
using System.Linq.Expressions;

namespace Ambev.Core.Domain.Interfaces;

public interface IBaseRepository<T> where T : BaseEntity
{
    void Create(T entity);
    void Update(T entity);
    void Delete(T entity);
    Task<T> Get(int id, CancellationToken cancellationToken);
    Task<List<T>> GetAll(CancellationToken cancellationToken);
    Task<List<T>> Filter(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);
}
