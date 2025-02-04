using System.Threading;
using NSubstitute;
using AutoMapper;
using Xunit;
using Ambev.Core.Domain.Entities;
using Ambev.Core.Domain.Interfaces;
using Ambev.Application.UseCases.Commands.Customer.CreateCustomer;

namespace Ambev.Core.Tests.Application.Handlers
{
    public class CreateCustomerHandlerTests
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        private readonly IJwtTokenService _tokenService;
        private readonly CreateCustomerHandler _handler;

        public CreateCustomerHandlerTests()
        {
            _unitOfWork = Substitute.For<IUnitOfWork>();
            _customerRepository = Substitute.For<ICustomerRepository>();
            _mapper = Substitute.For<IMapper>();
            _tokenService = Substitute.For<IJwtTokenService>();
            _handler = new CreateCustomerHandler(
                _unitOfWork,
                _customerRepository,
                _mapper,
                _tokenService
            );
        }

        [Fact]
        public async Task Handle_ValidRequest_ShouldCreateCustomerAndCommit()
        {
            // Arrange
            var request = new CreateCustomerRequest {  FirstName = "Euclides", LastName="Brasil", Identification="12345" };
            var customer = new Customer { Id = 1, FirstName = "Euclides", LastName = "Brasil", Identification = "12345" };
            var response = new CreateCustomerResponse {  FirstName = "Euclides", LastName = "Brasil", Identification = "12345" };
            response.setIdContext(1);
            _mapper.Map<Customer>(request).Returns(customer);
            _mapper.Map<CreateCustomerResponse>(customer).Returns(response);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.Equal(response, result);
            _customerRepository.Received(1).Create(customer);
            await _unitOfWork.Received(1).Commit(Arg.Any<CancellationToken>());
        }
    }
}
