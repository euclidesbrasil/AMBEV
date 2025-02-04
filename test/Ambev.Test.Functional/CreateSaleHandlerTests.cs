using Ambev.Core.Application.UseCases.Commands.Sale.CreateBranch;
using Ambev.Core.Application.UseCases.Commands.Sale.CreateSale;
using Ambev.Core.Application.UseCases.DTOs;
using Ambev.Core.Domain.Entities;
using Ambev.Core.Domain.Interfaces;
using AutoMapper;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Ambev.DeveloperEvaluation.Functional
{
    public class CreateSaleHandlerTests
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISaleRepository _saleRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IProductRepository _productRepository;
        private readonly IBranchRepository _branchRepository;
        private readonly IMapper _mapper;
        private readonly IProducerMessage _producerMessage;
        private readonly CreateSaleHandler _handler;

        public CreateSaleHandlerTests()
        {
            _unitOfWork = Substitute.For<IUnitOfWork>();
            _saleRepository = Substitute.For<ISaleRepository>();
            _customerRepository = Substitute.For<ICustomerRepository>();
            _productRepository = Substitute.For<IProductRepository>();
            _branchRepository = Substitute.For<IBranchRepository>();
            _mapper = Substitute.For<IMapper>();
            _producerMessage = Substitute.For<IProducerMessage>();
            _handler = new CreateSaleHandler(
                _unitOfWork,
                _saleRepository,
                _customerRepository,
                _productRepository,
                _branchRepository,
                _mapper,
                _producerMessage
            );
        }

        [Fact]
        public async Task Handle_ValidRequest_ShouldCreateSaleAndCommit()
        {
            // Arrange
            var request = new CreateSaleRequest
            {
                CustomerId = 1,
                BranchId = 1,
                Items = new List<SaleItemBaseDTO>
                {
                    new SaleItemDTO { ProductId = 1, Quantity = 2 },
                    new SaleItemDTO { ProductId = 2, Quantity = 3 }
                }
            };

            var sale = new Sale { Id=1,CustomerFirstName = "John", CustomerLastName = "Doe", BranchName = "Main Branch" };
            var customer = new Customer { Id = 1, FirstName = "John", LastName = "Doe" };
            var branch = new Branch { Id = 1, Name = "Main Branch" };
            var productsUsed = new List<Product>
            {
                new Product(1, "title", 10, "desc", "category", "http://image.com", new Core.Domain.ValueObjects.Rating(1, 2)),
                new Product(2, "title", 10, "desc", "category", "http://image.com", new Core.Domain.ValueObjects.Rating(1, 2))
            };
            var response = new CreateSaleResponse() { Id = 1 };
            
            _mapper.Map<Sale>(request).Returns(sale);
            _customerRepository.Get(request.CustomerId, Arg.Any<CancellationToken>()).Returns(customer);
            _branchRepository.Get(request.BranchId, Arg.Any<CancellationToken>()).Returns(branch);

            _productRepository.Filter(Arg.Any<Expression<Func<Product, bool>>>(), Arg.Any<CancellationToken>())
                .Returns(callInfo =>
                {
                    var predicate = callInfo.Arg<Expression<Func<Product, bool>>>();
                    return Task.FromResult(productsUsed.AsQueryable().Where(predicate.Compile()).ToList());
                });

            _mapper.Map<List<SaleItem>>(request.Items).Returns(new List<SaleItem>
            {
                new SaleItem { ProductId = 1, Quantity = 2 },
                new SaleItem { ProductId = 2, Quantity = 3 }
            });
            _mapper.Map<CreateSaleResponse>(sale).Returns(response);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.Equal(response, result);
            await _customerRepository.Received(1).Get(request.CustomerId, Arg.Any<CancellationToken>());
            await _branchRepository.Received(1).Get(request.BranchId, Arg.Any<CancellationToken>());
            await _productRepository.Received(1).Filter(Arg.Any<System.Linq.Expressions.Expression<System.Func<Product, bool>>>(), Arg.Any<CancellationToken>());
            _saleRepository.Received(1).Create(sale);
            await _producerMessage.Received(1).SendMessage(Arg.Any<object>(), "sale.created");
            await _unitOfWork.Received(1).Commit(Arg.Any<CancellationToken>());
        }

        [Fact]
        public async Task Handle_InvalidCustomer_ShouldThrowKeyNotFoundException()
        {
            // Arrange
            var request = new CreateSaleRequest { CustomerId = 999, BranchId = 1 };
            _customerRepository.Get(request.CustomerId, Arg.Any<CancellationToken>()).Returns((Customer)null);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() => _handler.Handle(request, CancellationToken.None));
            Assert.Equal($"Customer with ID {request.CustomerId} does not exist in our database", exception.Message);
        }

        [Fact]
        public async Task Handle_InvalidBranch_ShouldThrowKeyNotFoundException()
        {
            // Arrange
            var request = new CreateSaleRequest { CustomerId = 1, BranchId = 999 };
            var customer = new Customer { Id = 1, FirstName = "John", LastName = "Doe" };
            _customerRepository.Get(request.CustomerId, Arg.Any<CancellationToken>()).Returns(customer);
            _branchRepository.Get(request.BranchId, Arg.Any<CancellationToken>()).Returns((Branch)null);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() => _handler.Handle(request, CancellationToken.None));
            Assert.Equal($"Branch with ID {request.BranchId} does not exist in our database", exception.Message);
        }
    }
}

