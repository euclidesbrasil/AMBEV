using Ambev.Core.Domain.Interfaces;
using Ambev.Infrastructure.Persistence.PostgreSQL.Context;
using Microsoft.EntityFrameworkCore;
using Ambev.Core.Domain.Common;
using System.Linq;
using System.Linq.Dynamic.Core;
using Ambev.Core.Domain.Entities;
using Ambev.Infrastructure.Persistence.PostgreSQL.Extensions;

namespace Ambev.Infrastructure.Persistence.PostgreSQL.Repositories;

public class ProductRepository : BaseRepository<Product>, IProductRepository
{
    private readonly AppDbContext _context;
    public ProductRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<string>> GetProductCategoriesAsync(CancellationToken cancellationToken)
    {
        return await _context.Products
            .Select(p => p.Category)
            .Distinct()
            .ToListAsync(cancellationToken);
    }

    public async Task<List<Product>> GetProductByListIdsAsync(List<int> ids, CancellationToken cancellationToken)
    {
        return await _context.Products
            .Where(p => ids.Contains(p.Id))
            .ToListAsync(cancellationToken);
    }

    public async Task<PaginatedResult<Product>> GetProductsByCategoriesAsync(string nameCategory, PaginationQuery paginationQuery, CancellationToken cancellationToken)
    {
        var query = _context.Products.Where(x => x.Category.Equals(nameCategory));
        query = query.ApplyFilters(paginationQuery.Filter);

        paginationQuery.Order = paginationQuery.Order ?? "id asc";
        query = query.OrderBy(paginationQuery.Order);

        var totalCount = await query.CountAsync(cancellationToken); // Total de itens sem paginação
        var items = await query
            .Skip(paginationQuery.Skip)
            .Take(paginationQuery.Size)
            .ToListAsync(cancellationToken);

        return new PaginatedResult<Product>
        {
            Data = items,
            TotalItems = totalCount,
            CurrentPage = paginationQuery.Page
        };
    }
}
