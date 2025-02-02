using Ambev.Core.Domain.Common;
using Ambev.Core.Domain.Entities;

namespace Ambev.Core.Domain.Interfaces;

public interface ISaleRepository : IBaseRepository<Sale>
{
    Task<PaginatedResult<Sale>> GetSalesPagination(PaginationQuery paginationQuery, CancellationToken cancellationToken);
    Task<Sale> GetSaleWithItemsAsync(int saleId, CancellationToken cancellationToken);
}
