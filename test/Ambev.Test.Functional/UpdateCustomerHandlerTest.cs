using System.Threading;
using NSubstitute;
using AutoMapper;
using Xunit;
using Ambev.Core.Domain.Entities;
using Ambev.Core.Domain.Interfaces;
using Ambev.Application.UseCases.Commands.Customer.CreateCustomer;
using Ambev.Application.UseCases.Commands.Customer.UpdateCustomer;

namespace Ambev.Core.Tests.Application.Handlers
{
    public class UpdateCustomerHandlerTests
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        private readonly UpdateCustomerHandler _handler;

        public UpdateCustomerHandlerTests()
        {
            _unitOfWork = Substitute.For<IUnitOfWork>();
            _customerRepository = Substitute.For<ICustomerRepository>();
            _mapper = Substitute.For<IMapper>();
            _handler = new UpdateCustomerHandler(
                _unitOfWork,
                _customerRepository,
                _mapper
            );
        }

        [Fact]
        public async Task Handle_ValidRequest_ShouldUpdateCustomerAndCommit()
        {
            // Arrange
            var request = new UpdateCustomerRequest { Id = 1, FirstName = "John", LastName = "Doe", Identification = "12345" };
            var customer = new Customer { Id = 1, FirstName = "FirstName", LastName = "LastName", Identification = "Identification" };
            var response = new UpdateCustomerResponse { Id = 1, FirstName = "John", LastName = "Doe", Identification = "12345" };

            _customerRepository.Get(request.Id, Arg.Any<CancellationToken>()).Returns(customer);
            _mapper.Map<UpdateCustomerResponse>(customer).Returns(response);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            // Verificando se o resultado retornado está correto
            Assert.Equal(response.FirstName, request.FirstName);
            Assert.Equal(response.LastName, request.LastName);
            Assert.Equal(response.Identification, request.Identification);
            Assert.Equal(response.Id, request.Id);

            // Verificando se os métodos do repositório e UnitOfWork foram chamados
            await _customerRepository.Received(1).Get(request.Id, Arg.Any<CancellationToken>());
            await _unitOfWork.Received(1).Commit(Arg.Any<CancellationToken>());
        }

    }
}
