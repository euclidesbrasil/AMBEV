using Ambev.DeveloperEvaluation.Application.Users.CreateUser;
using Ambev.DeveloperEvaluation.Common.EventBroker;
using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Integration.Application;

/// <summary>
/// Contains unit tests for the <see cref="CreateUserHandler"/> class.
/// </summary>
public class CreateUserHandlerTests
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;
    private readonly CreateUserHandler _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateUserHandlerTests"/> class.
    /// Sets up the test dependencies and creates fake data generators.
    /// </summary>
    public CreateUserHandlerTests()
    {
        _userService = Substitute.For<IUserService>();
        _mapper = Substitute.For<IMapper>();        
        _handler = new CreateUserHandler(_userService, _mapper);
    }

    /// <summary>
    /// Tests that a valid user creation request is handled successfully.
    /// </summary>
    [Fact(DisplayName = "Given valid user data When creating user Then returns success response")]
    public async Task Handle_ValidRequest_ReturnsSuccessResponse()
    {
        // Given
        var command = CreateUserHandlerTestData.GenerateValidCommand();
        var user = CreateUserHandlerTestData.CreateUserFromCommand(command);

        var result = new CreateUserResult
        {
            Id = user.Id,
        };

        _mapper.Map<User>(command).Returns(user);
        _mapper.Map<CreateUserResult>(user).Returns(result);

        _userService.CreateUserAsync(Arg.Any<User>(), Arg.Any<CancellationToken>()).Returns(user);

        // When
        var createUserResult = await _handler.Handle(command, CancellationToken.None);

        // Then
        createUserResult.Should().NotBeNull();
        createUserResult.Id.Should().Be(user.Id);
    }

    /// <summary>
    /// Tests that an invalid user creation request throws a validation exception.
    /// </summary>
    [Fact(DisplayName = "Given invalid user data When creating user Then throws validation exception")]
    public async Task Handle_InvalidRequest_ThrowsValidationException()
    {
        // Given
        var command = new CreateUserCommand(); // Empty command will fail validation

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<FluentValidation.ValidationException>();
    }

    /// <summary>
    /// Tests that the password is hashed before saving the user.
    /// </summary>
    [Fact(DisplayName = "Given user creation request When handling Then password is hashed")]
    public async Task Handle_ValidRequest_HashesPassword()
    {
        // Given
        var command = CreateUserHandlerTestData.GenerateValidCommand();
        const string hashedPassword = "h@shedPassw0rd";
        var user = CreateUserHandlerTestData.CreateUserFromCommand(command);

        _mapper.Map<User>(command).Returns(user);
        _userService.CreateUserAsync(Arg.Any<User>(), Arg.Any<CancellationToken>()).Returns(user);

        // When
        await _handler.Handle(command, CancellationToken.None);

        // Then
        await _userService.Received(1).CreateUserAsync(
            Arg.Is<User>(u => u.Password == hashedPassword),
            Arg.Any<CancellationToken>());
    }

    /// <summary>
    /// Tests that the mapper is called with the correct command.
    /// </summary>
    [Fact(DisplayName = "Given valid command When handling Then maps command to user entity")]
    public async Task Handle_ValidRequest_MapsCommandToUser()
    {
        // Given
        var command = CreateUserHandlerTestData.GenerateValidCommand();
        var user = CreateUserHandlerTestData.CreateUserFromCommand(command);

        _mapper.Map<User>(command).Returns(user);
        _userService.CreateUserAsync(Arg.Any<User>(), Arg.Any<CancellationToken>()).Returns(user);

        // When
        await _handler.Handle(command, CancellationToken.None);

        // Then
        _mapper.Received(1).Map<User>(Arg.Is<CreateUserCommand>(c =>
            c.Username == command.Username &&
            c.Email == command.Email &&
            c.Phone == command.Phone &&
            c.Status == command.Status &&
            c.Role == command.Role));
    }
}
