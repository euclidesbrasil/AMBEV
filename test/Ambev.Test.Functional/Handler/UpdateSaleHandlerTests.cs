using Ambev.Core.Application.UseCases.Commands.Sale.CreateBranch;
using Ambev.Core.Application.UseCases.Commands.Sale.CreateSale;
using Ambev.Core.Application.UseCases.Commands.Sale.UpdateSale;
using Ambev.Core.Application.UseCases.DTOs;
using Ambev.Core.Domain.Entities;
using Ambev.Core.Domain.Interfaces;
using AutoMapper;
using Castle.Core.Resource;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Handler
{
    public class UpdateSaleHandlerTests
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISaleRepository _saleRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IProductRepository _productRepository;
        private readonly IBranchRepository _branchRepository;
        private readonly IMapper _mapper;
        private readonly IProducerMessage _producerMessage;
        private readonly UpdateSaleHandler _handler;

        public UpdateSaleHandlerTests()
        {
            _unitOfWork = Substitute.For<IUnitOfWork>();
            _saleRepository = Substitute.For<ISaleRepository>();
            _customerRepository = Substitute.For<ICustomerRepository>();
            _productRepository = Substitute.For<IProductRepository>();
            _branchRepository = Substitute.For<IBranchRepository>();
            _mapper = Substitute.For<IMapper>();
            _producerMessage = Substitute.For<IProducerMessage>();
            _handler = new UpdateSaleHandler(
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
        public async Task Handle_ValidRequest_ShouldUpdateSaleAndCommit()
        {
            // Arrange
            var request = new UpdateSaleRequest
            {
                Id = 1,
                CustomerId = 1,
                BranchId = 1,
                Items = new List<UpdateSaleItemRequest>
                {
                    new UpdateSaleItemRequest { ProductId = 1, Quantity = 2,Id =1 },
                    new UpdateSaleItemRequest { ProductId = 2, Quantity = 2, Id=2 },
                }
            };
            var saleToUpdate = new Sale
            {
                Id = 1,
                CustomerId = 1,
                CustomerFirstName = "John",
                CustomerLastName = "Doe",
                BranchName = "Main Branch",
                BranchId = 1,
                Items = new List<SaleItem>()
                {
                new SaleItem { ProductId = 1, Quantity = 2, Id =1 },
                new SaleItem { ProductId = 2, Quantity = 3, Id =2 }
                }
            };
            var sale = new Sale
            {
                Id = 1,
                CustomerFirstName = "John",
                CustomerLastName = "Doe",
                BranchName = "Main Branch",
                Items = new List<SaleItem>()
                {
                new SaleItem { ProductId = 1, Quantity = 2, Id =1 },
                new SaleItem { ProductId = 2, Quantity = 3, Id =2 }
                }
            };
            var customer = new Customer { Id = 1, FirstName = "John", LastName = "Doe" };
            var branch = new Branch { Id = 1, Name = "Main Branch" };
            var productsUsed = new List<Product>
            {
               new Product(1, "title", 10, "desc", "category", "http://image.com", new Core.Domain.ValueObjects.Rating(1, 2)),
                new Product(2, "title", 10, "desc", "category", "http://image.com", new Core.Domain.ValueObjects.Rating(1, 2))
            };
            var response = new UpdateSaleResponse
            {
                Id = 1,
                CustomerId = 1,
                BranchId = 1,
                Items = new List<SaleItemDTO>()
                {
                     new SaleItemDTO { ProductId = 1, Quantity = 2, Id =1 },
                     new SaleItemDTO { ProductId = 2, Quantity = 3, Id =2 }
                }
            };

            _saleRepository.GetSaleWithItemsAsync(request.Id, Arg.Any<CancellationToken>()).Returns(sale);
            _mapper.Map<Sale>(request).Returns(saleToUpdate);
            _customerRepository.Get(request.CustomerId, Arg.Any<CancellationToken>()).Returns(customer);
            _branchRepository.Get(saleToUpdate.BranchId, Arg.Any<CancellationToken>()).Returns(branch);
            _productRepository.Filter(Arg.Any<Expression<Func<Product, bool>>>(), Arg.Any<CancellationToken>())
                .Returns(callInfo =>
                {
                    var predicate = callInfo.Arg<Expression<Func<Product, bool>>>();
                    return Task.FromResult(productsUsed.AsQueryable().Where(predicate.Compile()).ToList());
                });
            _mapper.Map<List<SaleItem>>(request.Items).Returns(new List<SaleItem>
            {
                new SaleItem { Id = 1, ProductId = 1, Quantity = 2 },
                new SaleItem { Id = 2, ProductId = 2, Quantity = 3 }
            });
            _mapper.Map<UpdateSaleResponse>(sale).Returns(response);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.Equal(response, result);
            await _saleRepository.Received(1).GetSaleWithItemsAsync(request.Id, Arg.Any<CancellationToken>());
            await _customerRepository.Received(1).Get(request.CustomerId, Arg.Any<CancellationToken>());
            await _branchRepository.Received(1).Get(saleToUpdate.BranchId, Arg.Any<CancellationToken>());
            await _productRepository.Received(1).Filter(Arg.Any<Expression<Func<Product, bool>>>(), Arg.Any<CancellationToken>());
            _saleRepository.Received(1).Update(sale);
            await _unitOfWork.Received(1).Commit(Arg.Any<CancellationToken>());
        }

        [Fact]
        public async Task Handle_InvalidCustomer_ShouldThrowKeyNotFoundException()
        {

            // Arrange
            var request = new UpdateSaleRequest
            {
                Id = 1,
                CustomerId = 999,
                BranchId = 1,
                Items = new List<UpdateSaleItemRequest>()
                {
                new UpdateSaleItemRequest { ProductId = 1, Quantity = 2, Id =1 },
                new UpdateSaleItemRequest { ProductId = 2, Quantity = 3, Id =2 }
                }
            };
            var saleToUpdate = new Sale
            {
                Id = 1,
                CustomerId = 1,
                CustomerFirstName = "John",
                CustomerLastName = "Doe",
                BranchName = "Main Branch",
                BranchId = 1,
                Items = new List<SaleItem>()
                {
                new SaleItem { ProductId = 1, Quantity = 2, Id =1 },
                new SaleItem { ProductId = 2, Quantity = 3, Id =2 }
                }
            };
            var productsUsed = new List<Product>
            {
               new Product(1, "title", 10, "desc", "category", "http://image.com", new Core.Domain.ValueObjects.Rating(1, 2)),
                new Product(2, "title", 10, "desc", "category", "http://image.com", new Core.Domain.ValueObjects.Rating(1, 2))
            };
            var sale = new Sale
            {
                Id = 1,
                CustomerFirstName = "John",
                CustomerLastName = "Doe",
                BranchName = "Main Branch",
                Items = new List<SaleItem>()
                {
                new SaleItem { ProductId = 1, Quantity = 2, Id =1 },
                new SaleItem { ProductId = 2, Quantity = 3, Id =2 }
                }
            };
            var customer = new Customer { Id = 1, FirstName = "John", LastName = "Doe" };
            var branch = new Branch { Id = 1, Name = "Main Branch" };
            _saleRepository.GetSaleWithItemsAsync(request.Id, Arg.Any<CancellationToken>()).Returns(sale);
            _mapper.Map<Sale>(request).Returns(saleToUpdate);
            _customerRepository.Get(request.CustomerId, Arg.Any<CancellationToken>()).Returns(customer);
            _branchRepository.Get(saleToUpdate.BranchId, Arg.Any<CancellationToken>()).Returns(branch);
            _productRepository.Filter(Arg.Any<Expression<Func<Product, bool>>>(), Arg.Any<CancellationToken>())
                .Returns(callInfo =>
                {
                    var predicate = callInfo.Arg<Expression<Func<Product, bool>>>();
                    return Task.FromResult(productsUsed.AsQueryable().Where(predicate.Compile()).ToList());
                });
            _mapper.Map<List<SaleItem>>(request.Items).Returns(new List<SaleItem>
            {
                new SaleItem { Id = 1, ProductId = 1, Quantity = 2 },
                new SaleItem { Id = 2, ProductId = 2, Quantity = 3 }
            });
            _customerRepository.Get(request.CustomerId, Arg.Any<CancellationToken>()).Returns((Customer)null);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() => _handler.Handle(request, CancellationToken.None));
            Assert.Equal($"Customer with ID {request.CustomerId} does not exist in our database", exception.Message);
        }

        [Fact]
        public async Task Handle_InvalidBranch_ShouldThrowKeyNotFoundException()
        {
            // Arrange
            // Arrange
            var request = new UpdateSaleRequest
            {
                Id = 1,
                CustomerId = 1,
                BranchId = 998,
                Items = new List<UpdateSaleItemRequest>()
                {
                new UpdateSaleItemRequest { ProductId = 1, Quantity = 2, Id =1 },
                new UpdateSaleItemRequest { ProductId = 2, Quantity = 3, Id =2 }
                }
            };
            var saleToUpdate = new Sale
            {
                Id = 1,
                CustomerId = 1,
                CustomerFirstName = "John",
                CustomerLastName = "Doe",
                BranchName = "Main Branch",
                BranchId = 999,
                Items = new List<SaleItem>()
                {
                new SaleItem { ProductId = 1, Quantity = 2, Id =1 },
                new SaleItem { ProductId = 2, Quantity = 3, Id =2 }
                }
            };
            var productsUsed = new List<Product>
            {
               new Product(1, "title", 10, "desc", "category", "http://image.com", new Core.Domain.ValueObjects.Rating(1, 2)),
                new Product(2, "title", 10, "desc", "category", "http://image.com", new Core.Domain.ValueObjects.Rating(1, 2))
            };
            var sale = new Sale
            {
                Id = 1,
                CustomerFirstName = "John",
                CustomerLastName = "Doe",
                BranchName = "Main Branch",
                Items = new List<SaleItem>()
                {
                new SaleItem { ProductId = 1, Quantity = 2, Id =1 },
                new SaleItem { ProductId = 2, Quantity = 3, Id =2 }
                }
            };
            var customer = new Customer { Id = 1, FirstName = "John", LastName = "Doe" };
            var branch = new Branch { Id = 1, Name = "Main Branch" };
            _saleRepository.GetSaleWithItemsAsync(request.Id, Arg.Any<CancellationToken>()).Returns(sale);
            _mapper.Map<Sale>(request).Returns(saleToUpdate);
            _customerRepository.Get(request.CustomerId, Arg.Any<CancellationToken>()).Returns(customer);
            _branchRepository.Get(request.BranchId, Arg.Any<CancellationToken>()).Returns((Branch)null);
            _productRepository.Filter(Arg.Any<Expression<Func<Product, bool>>>(), Arg.Any<CancellationToken>())
                .Returns(callInfo =>
                {
                    var predicate = callInfo.Arg<Expression<Func<Product, bool>>>();
                    return Task.FromResult(productsUsed.AsQueryable().Where(predicate.Compile()).ToList());
                });
            _mapper.Map<List<SaleItem>>(request.Items).Returns(new List<SaleItem>
            {
                new SaleItem { Id = 1, ProductId = 1, Quantity = 2 },
                new SaleItem { Id = 2, ProductId = 2, Quantity = 3 }
            });

            // Act & Assert
            var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() => _handler.Handle(request, CancellationToken.None));
            Assert.Equal($"Branch with ID {request.BranchId} does not exist in our database", exception.Message);
        }
    }
}

