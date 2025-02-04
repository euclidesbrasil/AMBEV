using Ambev.Application.UseCases.Commands.User.CreateUser;
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

namespace Ambev.DeveloperEvaluation.Functional
{
    public class CreateUserHandlerTests
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IJwtTokenService _tokenService;
        private readonly CreateUserHandler _handler;

        public CreateUserHandlerTests()
        {
            _unitOfWork = Substitute.For<IUnitOfWork>();
            _userRepository = Substitute.For<IUserRepository>();
            _mapper = Substitute.For<IMapper>();
            _tokenService = Substitute.For<IJwtTokenService>();
            _handler = new CreateUserHandler(_unitOfWork, _userRepository, _mapper, _tokenService);
        }

        [Fact]
        public async Task Handle_Should_CreateUser_When_RequestIsValid()
        {
            // Arrange
            var request = new CreateUserRequest
            {
                Username = "testuser",
                Email = "test@example.com",
                Password = "Password123",
                Name = new Core.Application.UseCases.DTOs.NameDto { Firstname = "Test", Lastname = "User" },
                Address = new Core.Application.UseCases.DTOs.AddressDto() { City = "City", Geolocation = new Core.Application.UseCases.DTOs.GeolocationDto() { Lat = "1.0", Long = "2.0" }, Number = 1, Street = "Street", Zipcode = "606060660" },
                Status = UserStatus.Active,
                Role = UserRole.Admin
            };
            var _user = new Ambev.Core.Domain.Entities.User(
                "testuser",
                "test@example.com",
                "Password123", "Test", "User",
                new Core.Domain.ValueObjects.Address("City", "Street", 1, "606060660",
                new Core.Domain.ValueObjects.Geolocation("1.0", "2.0")), "11111",
                UserStatus.Active, UserRole.Admin, _tokenService);

            _mapper.Map<Ambev.Core.Domain.Entities.User>(request).Returns(_user);
            _userRepository.GetByLoginAsync(request.Username).Returns(Task.FromResult<Ambev.Core.Domain.Entities.User>(null));
            _userRepository.GetByEmailAsync(request.Email).Returns(Task.FromResult<Ambev.Core.Domain.Entities.User>(null));

            // Act
            var response = await _handler.Handle(request, CancellationToken.None);

            // Assert
            await _unitOfWork.Received().Commit(CancellationToken.None);
            _userRepository.Received().Create(Arg.Any<Ambev.Core.Domain.Entities.User>());
            _mapper.Received().Map<CreateUserResponse>(Arg.Any<Ambev.Core.Domain.Entities.User>());
        }

        [Fact]
        public async Task Handle_Should_ThrowException_When_UsernameAlreadyTaken()
        {
            // Arrange
            var request = new CreateUserRequest
            {
                Username = "testuser",
                Email = "test@example.com",
                Password = "Password123",
                Name = new Core.Application.UseCases.DTOs.NameDto { Firstname = "Test", Lastname = "User" },
                Address = new Core.Application.UseCases.DTOs.AddressDto() { City = "City", Geolocation = new Core.Application.UseCases.DTOs.GeolocationDto() { Lat = "1.0", Long = "2.0" }, Number = 1, Street = "Street", Zipcode = "606060660" },
                Status = UserStatus.Active,
                Role = UserRole.Admin
            };
            var _user = new Ambev.Core.Domain.Entities.User(
                "testuser2",
                "test@example.com",
                "Password123", "Test", "User",
                new Core.Domain.ValueObjects.Address("City", "Street", 1, "606060660",
                new Core.Domain.ValueObjects.Geolocation("1.0", "2.0")), "11111",
                UserStatus.Active, UserRole.Admin, _tokenService);

            _mapper.Map<Ambev.Core.Domain.Entities.User>(request).Returns(_user);
            _userRepository.GetByLoginAsync(request.Username).Returns(Task.FromResult<Ambev.Core.Domain.Entities.User>(_user));
            _userRepository.GetByEmailAsync(request.Email).Returns(Task.FromResult<Ambev.Core.Domain.Entities.User>(_user));

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => _handler.Handle(request, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_Should_ThrowException_When_EmailAlreadyTaken()
        {
            // Arrange
            var request = new CreateUserRequest
            {
                Username = "testuser",
                Email = "test@example.com",
                Password = "Password123",
                Name = new Core.Application.UseCases.DTOs.NameDto { Firstname = "Test", Lastname = "User" },
                Address = new Core.Application.UseCases.DTOs.AddressDto() { City = "City", Geolocation = new Core.Application.UseCases.DTOs.GeolocationDto() { Lat = "1.0", Long = "2.0" }, Number = 1, Street = "Street", Zipcode = "606060660" },
                Status = UserStatus.Active,
                Role = UserRole.Admin
            };
            var _user = new Ambev.Core.Domain.Entities.User(
                "testuser",
                "test@example.com",
                "Password123", "Test", "User",
                new Core.Domain.ValueObjects.Address("City", "Street", 1, "606060660",
                new Core.Domain.ValueObjects.Geolocation("1.0", "2.0")), "11111",
                UserStatus.Active, UserRole.Admin, _tokenService);

            _mapper.Map<Ambev.Core.Domain.Entities.User>(request).Returns(_user);
            _userRepository.GetByLoginAsync(request.Username).Returns(Task.FromResult<Ambev.Core.Domain.Entities.User>(_user));
            _userRepository.GetByEmailAsync(request.Email).Returns(Task.FromResult<Ambev.Core.Domain.Entities.User>(_user));

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => _handler.Handle(request, CancellationToken.None));
        }
    }
}

