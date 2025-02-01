using Ambev.Core.Domain.Common;
using Ambev.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.Core.Domain.Interfaces
{
    public interface ICartRepository: IBaseRepositoryNoRelational<Cart>
    {
        Task<PaginatedResult<Cart>> GetPaginatedResultAsync(Expression<Func<Cart, bool>> filter, PaginationQuery paginationQuery, CancellationToken cancellationToken);
    }
}
