using Ambev.Core.Domain.Common;
using Ambev.Core.Domain.Entities;

namespace Ambev.Core.Domain.Interfaces;

public interface IProductRepository : IBaseRepository<Product>
{
    Task<List<string>> GetProductCategoriesAsync(CancellationToken cancellationToken);
    Task<PaginatedResult<Product>> GetProductsByCategoriesAsync(string nameCategory, PaginationQuery paginationQuery, CancellationToken cancellationToken);
}
