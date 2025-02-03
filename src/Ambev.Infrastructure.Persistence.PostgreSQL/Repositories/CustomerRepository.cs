using Ambev.Core.Domain.Interfaces;
using Ambev.Infrastructure.Persistence.PostgreSQL.Context;
using Microsoft.EntityFrameworkCore;
using Ambev.Core.Domain.Common;
using System.Linq;
using Ambev.Core.Domain.Entities;
using System.Linq.Dynamic.Core;

namespace Ambev.Infrastructure.Persistence.PostgreSQL.Repositories;

public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
{
    private readonly AppDbContext _context;
    public CustomerRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }
    public async Task<PaginatedResult<Customer>> GetCustumerPagination(PaginationQuery paginationQuery, CancellationToken cancellationToken)
    {
        var query = _context.Custumer.Where(x => true);
        paginationQuery.Order = paginationQuery.Order ?? "id asc";
        query = query.OrderBy(paginationQuery.Order);
        var totalCount = await query.CountAsync(cancellationToken); // Total de itens sem paginação
        var items = await query
            .Skip(paginationQuery.Skip)
            .Take(paginationQuery.Size)
            .ToListAsync(cancellationToken);

        return new PaginatedResult<Customer>
        {
            Data = items,
            TotalItems = totalCount,
            CurrentPage = paginationQuery.Page
        };
    }
}
