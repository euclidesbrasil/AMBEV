using Ambev.Core.Domain.Interfaces;
using Ambev.Infrastructure.Persistence.PostgreSQL.Context;
using Microsoft.EntityFrameworkCore;
using Ambev.Core.Domain.Common;
using System.Linq;
using Ambev.Core.Domain.Entities;
using System.Linq.Dynamic.Core;
using Ambev.Infrastructure.Persistence.PostgreSQL.Extensions;

namespace Ambev.Infrastructure.Persistence.PostgreSQL.Repositories;

public class SaleRepository : BaseRepository<Sale>, ISaleRepository
{
    private readonly AppDbContext _context;
    public SaleRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }
    public async Task<PaginatedResult<Sale>> GetSalesPagination(PaginationQuery paginationQuery, CancellationToken cancellationToken)
    {
        var query = _context.Sales.Where(x => true);
        query = query.ApplyFilters(paginationQuery.Filter);

        paginationQuery.Order = paginationQuery.Order ?? "id asc";
        query = query.OrderBy(paginationQuery.Order);
        var totalCount = await query.CountAsync(cancellationToken); // Total de itens sem paginação
        var items = await query
            .Skip(paginationQuery.Skip)
            .Take(paginationQuery.Size)
            .ToListAsync(cancellationToken);

        return new PaginatedResult<Sale>
        {
            Data = items,
            TotalItems = totalCount,
            CurrentPage = paginationQuery.Page
        };
    }

    public async Task<Sale> GetSaleWithItemsAsync(int id, CancellationToken cancellationToken)
    {
        return await _context.Sales
            .Include(s => s.Items)
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
    }
}
