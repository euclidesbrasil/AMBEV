using Ambev.Core.Domain.Common;
using Ambev.Core.Domain.Entities;

namespace Ambev.Core.Domain.Interfaces;

public interface ICustomerRepository : IBaseRepository<Customer>
{
    Task<PaginatedResult<Customer>> GetCustumerPagination(PaginationQuery paginationQuery, CancellationToken cancellationToken);
}
