using Ambev.Application.UseCases.Commands.Customer.DeleteCustomer;
using Ambev.Core.Domain.Entities;
using Ambev.Core.Domain.Interfaces;
using AutoMapper;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Handler
{
    public class DeleteCustomerHandlerTests
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        private readonly DeleteCustomerHandler _handler;

        public DeleteCustomerHandlerTests()
        {
            _unitOfWork = Substitute.For<IUnitOfWork>();
            _customerRepository = Substitute.For<ICustomerRepository>();
            _mapper = Substitute.For<IMapper>();
            _handler = new DeleteCustomerHandler(
                _unitOfWork,
                _customerRepository,
                _mapper
            );
        }

        [Fact]
        public async Task Handle_ValidRequest_ShouldDeleteCustomerAndCommit()
        {
            // Arrange
            var request = new DeleteCustomerRequest(1);
            var customer = new Customer { Id = 1, FirstName = "John", LastName = "Doe", Identification = "12345" };
            var response = new DeleteCustomerResponse("Customer deleted with success.");

            _customerRepository.Get(request.id, Arg.Any<CancellationToken>()).Returns(customer);
            _mapper.Map<DeleteCustomerResponse>(customer).Returns(response);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.Equal(response, result);
            await _customerRepository.Received(1).Get(request.id, Arg.Any<CancellationToken>());
            _customerRepository.Received(1).Delete(customer);
            await _unitOfWork.Received(1).Commit(Arg.Any<CancellationToken>());
        }

        [Fact]
        public async Task Handle_InvalidRequest_ShouldThrowKeyNotFoundException()
        {
            // Arrange
            var request = new DeleteCustomerRequest(999); // ID que não existe
            _customerRepository.Get(request.id, Arg.Any<CancellationToken>()).Returns((Customer)null);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() => _handler.Handle(request, CancellationToken.None));
            Assert.Equal($"Custumer with ID {request.id} does not exist in our database", exception.Message);
        }
    }
}
