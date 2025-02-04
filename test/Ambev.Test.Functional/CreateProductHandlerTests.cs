using Ambev.Application.UseCases.Commands.Product.CreateProduct;
using Ambev.Core.Application.UseCases.Commands.Sale.CreateBranch;
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

namespace Ambev.DeveloperEvaluation.Functional
{
    public class CreateProductHandlerTests
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly CreateProductHandler _handler;

        public CreateProductHandlerTests()
        {
            _unitOfWork = Substitute.For<IUnitOfWork>();
            _productRepository = Substitute.For<IProductRepository>();
            _mapper = Substitute.For<IMapper>();
            _handler = new CreateProductHandler(
                _unitOfWork,
                _productRepository,
                _mapper
            );
        }

        [Fact]
        public async Task Handle_ValidRequest_ShouldCreateProductAndCommit()
        {
            // Arrange
            var request = new CreateProductRequest { Category = "category", Description = "desc", Image = "http://image.com", Price = 10, Rating = new Core.Application.UseCases.DTOs.RatingDTO() { Count = 1, Rate = 2 }, Title = "title" };
            var product = new Product(1, "title", 10, "desc", "category", "http://image.com", new Core.Domain.ValueObjects.Rating(1, 2));
            var response = new CreateProductResponse { Id = 1, Category = "category", Description = "desc", Image = "http://image.com", Price = 10, Rating = new Core.Application.UseCases.DTOs.RatingDTO() { Count = 1, Rate = 2 }, Title = "title" };

            _mapper.Map<Product>(request).Returns(product);
            _mapper.Map<CreateProductResponse>(product).Returns(response);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.Equal(response, result);
            _productRepository.Received(1).Create(product);
            await _unitOfWork.Received(1).Commit(Arg.Any<CancellationToken>());
        }
    }
}

