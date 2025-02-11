using Ambev.Core.Domain.Interfaces;
using Ambev.Core.Domain.Common;
using System.Linq;
using Ambev.Core.Domain.Entities;
using System.Linq.Expressions;
using MongoDB.Driver;
using Ambev.Infrastructure.Persistence.MongoDB.Service;

namespace Ambev.Infrastructure.Persistence.Repositories;

public class CartRepository : BaseRepositoryNoRelational<Cart>, ICartRepository
{
    private readonly CounterService _counterService;
    public CartRepository(IMongoDatabase database, CounterService counterService)
        : base(database, "Carts")
    {
        _counterService = counterService;
    }

    public override async Task Create(Cart entity)
    {
        // Gera o próximo valor de Id autoincrementável
        entity.Id = await _counterService.GetNextSequenceValueAsync("Carts");
        await base.Create(entity);
    }

    public async Task<PaginatedResult<Cart>> GetPaginatedResultAsync(
    Expression<Func<Cart, bool>> filter,
    PaginationQuery paginationQuery,
    CancellationToken cancellationToken)
    {
        var query = _collection.Find(filter);

        /*
        if (!string.IsNullOrEmpty(paginationQuery.Order))
        {
            var sortDefinition = paginationQuery.OrderAscending
                ? Builders<Cart>.Sort.Ascending(paginationQuery.Order)
                : Builders<Cart>.Sort.Descending(paginationQuery.Order);

            query = query.Sort(sortDefinition);
        }
        */
        var totalCount = await query.CountDocumentsAsync(cancellationToken);

        var items = await query
            .Skip(paginationQuery.Skip)
            .Limit(paginationQuery.Size)
            .ToListAsync(cancellationToken);

        return new PaginatedResult<Cart>
        {
            Data = items,
            TotalItems = (int)totalCount,
            CurrentPage = paginationQuery.Page
        };
    }
}
