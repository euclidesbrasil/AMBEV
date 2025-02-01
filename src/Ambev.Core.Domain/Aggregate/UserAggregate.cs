using Ambev.Core.Domain.Entities;
using Ambev.Core.Domain.Enum;
using Ambev.Core.Domain.Interfaces;
using Ambev.Core.Domain.ValueObjects;

namespace Ambev.Core.Domain.Aggregate
{
    public class UserAggregate
    {
        public User User { get; private set; }

        private UserAggregate() { } // Para EF Core

        public UserAggregate(string email, string username, string password, string firstname, string lastname,
            Address address, string phone, UserStatus status, UserRole role, IJwtTokenService _tokenService)
        {
            User = new User(email, username, password, firstname, lastname, address, phone, status, role, _tokenService);
        }

        public void UpdateUserInfo(string firstname, string lastname, Address address, string phone, UserStatus status, UserRole role)
        {
            User.UpdateUserInfo(firstname, lastname, address, phone, status, role);
        }
    }
}
