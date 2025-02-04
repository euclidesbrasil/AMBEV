using Ambev.Application.UseCases.Commands.Product.DeleteProduct;
using Ambev.Core.Application.UseCases.Commands.Branch.DeleteBranch;
using Ambev.Core.Application.UseCases.Commands.Sale.CreateBranch;
using Ambev.Core.Domain.Entities;
using Ambev.Core.Domain.Interfaces;
using Ambev.Core.Domain.ValueObjects;
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
    public class DeleteProductHandlerTests
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly DeleteProductHandler _handler;

        public DeleteProductHandlerTests()
        {
            _unitOfWork = Substitute.For<IUnitOfWork>();
            _productRepository = Substitute.For<IProductRepository>();
            _mapper = Substitute.For<IMapper>();
            _handler = new DeleteProductHandler(
                _unitOfWork,
                _productRepository,
                _mapper
            );
        }

        [Fact]
        public async Task Handle_ValidRequest_ShouldDeleteProductAndCommit()
        {
            // Arrange
            var request = new DeleteProductRequest(1);
            var product = new Product(1, "title", 10, "desc", "category", "http://image.com", new Rating(1, 2));
            var response = new DeleteProductResponse("Product deleted with success.");

            _productRepository.Get(request.id, Arg.Any<CancellationToken>()).Returns(product);
            _mapper.Map<DeleteProductResponse>(product).Returns(response);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.Equal(response, result);
            await _productRepository.Received(1).Get(request.id, Arg.Any<CancellationToken>());
            _productRepository.Received(1).Delete(product);
            await _unitOfWork.Received(1).Commit(Arg.Any<CancellationToken>());
        }

        [Fact]
        public async Task Handle_InvalidRequest_ShouldThrowKeyNotFoundException()
        {
            // Arrange
            var request = new DeleteProductRequest(999); // ID que não existe
            _productRepository.Get(request.id, Arg.Any<CancellationToken>()).Returns((Product)null);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() => _handler.Handle(request, CancellationToken.None));
            Assert.Equal($"Product with ID {request.id} does not exist in our database", exception.Message);
        }
    }
}

