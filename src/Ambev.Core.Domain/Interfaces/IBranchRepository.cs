using Ambev.Core.Domain.Common;
using Ambev.Core.Domain.Entities;

namespace Ambev.Core.Domain.Interfaces;

public interface IBranchRepository : IBaseRepository<Branch>
{
    Task<PaginatedResult<Branch>> GetBranchPagination(PaginationQuery paginationQuery, CancellationToken cancellationToken);
}
