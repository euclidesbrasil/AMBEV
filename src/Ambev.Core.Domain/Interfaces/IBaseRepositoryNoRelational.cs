﻿using Ambev.Core.Domain.Common;
using Ambev.Core.Domain.Entities;
using System.Linq.Expressions;

namespace Ambev.Core.Domain.Interfaces;

public interface IBaseRepositoryNoRelational<T> where T : BaseEntityNoRelational
{
    Task Create(T entity);
    Task Update(T entity);
    Task Delete(T entity);
    Task<List<T>> GetAll(CancellationToken cancellationToken);
    Task<List<T>> Filter(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);
}
