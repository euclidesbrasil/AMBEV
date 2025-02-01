using Ambev.Core.Domain.Common;
using System.Linq.Expressions;

namespace Ambev.Core.Domain.Interfaces;

public interface IPaginatedRepository<T>
{
    Task<PaginatedResult<T>> GetPaginatedAsync(PaginationQuery paginationQuery, CancellationToken cancellationToken);
}
