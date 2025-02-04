using Ambev.Application.UseCases.Commands.User.CreateUser;
using Ambev.Application.UseCases.Commands.User.UpdateUser;
using Ambev.Core.Application.UseCases.Commands.Sale.CreateBranch;
using Ambev.Core.Domain.Common;
using Ambev.Core.Domain.Entities;
using Ambev.Core.Domain.Enum;
using Ambev.Core.Domain.Interfaces;
using AutoMapper;
using Bogus.DataSets;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Handler
{
    public class UpdateUserHandlerTests
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IJwtTokenService _tokenService;
        private readonly UpdateUserHandler _handler;

        public UpdateUserHandlerTests()
        {
            _unitOfWork = Substitute.For<IUnitOfWork>();
            _userRepository = Substitute.For<IUserRepository>();
            _mapper = Substitute.For<IMapper>();
            _tokenService = Substitute.For<IJwtTokenService>();
            _handler = new UpdateUserHandler(_unitOfWork, _userRepository, _mapper, _tokenService);
        }

        [Fact]
        public async Task Handle_Should_UpdateUser_When_RequestIsValid()
        {
            // Arrange
            var request = new UpdateUserRequest
            {
                Id = 1,
                Username = "testuser",
                Email = "test@example.com",
                Password = "Password123",
                Name = new Core.Application.UseCases.DTOs.NameDto { Firstname = "Test", Lastname = "User" },
                Address = new Core.Application.UseCases.DTOs.AddressDto() { City = "City", Geolocation = new Core.Application.UseCases.DTOs.GeolocationDto() { Lat = "1.0", Long = "2.0" }, Number = 1, Street = "Street", Zipcode = "606060660" },
                Status = UserStatus.Active,
                Role = UserRole.Admin
            };
            var user = new User(
                "testuser",
                "test@example.com",
                "Password123", "Test", "User",
                new Core.Domain.ValueObjects.Address("City", "Street", 1, "606060660",
                new Core.Domain.ValueObjects.Geolocation("1.0", "2.0")), "11111",
                UserStatus.Active, UserRole.Admin, _tokenService);
            user.Id = 1;
            _mapper.Map<User>(request).Returns(user);

            // Act
            var response = await _handler.Handle(request, CancellationToken.None);

            // Assert
            _userRepository.Received().Update(user);
            await _unitOfWork.Received().Commit(CancellationToken.None);
            _mapper.Received().Map<UpdateUserResponse>(request);
        }
    }
}
