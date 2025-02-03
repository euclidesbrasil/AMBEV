using Ambev.Core.Domain.Common;
using Ambev.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.Core.Domain.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> GetByEmailAsync(string email);
        Task<User> GetByLoginAsync(string login);
        Task<PaginatedResult<User>> GetUsersPagination(PaginationQuery paginationQuery, CancellationToken cancellationToken);
    }
}
