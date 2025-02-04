using Ambev.Application.UseCases.Commands.Product.CreateProduct;
using Ambev.Application.UseCases.Commands.Product.UpdateProduct;
using Ambev.Core.Application.UseCases.Commands.Sale.CreateBranch;
using Ambev.Core.Application.UseCases.DTOs;
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
    public class UpdateProductHandlerTests
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly UpdateProductHandler _handler;

        public UpdateProductHandlerTests()
        {
            _unitOfWork = Substitute.For<IUnitOfWork>();
            _productRepository = Substitute.For<IProductRepository>();
            _mapper = Substitute.For<IMapper>();
            _handler = new UpdateProductHandler(
                _unitOfWork,
                _productRepository,
                _mapper
            );
        }

        [Fact]
        public async Task Handle_ValidRequest_ShouldUpdateProductAndCommit()
        {
            // Arrange
            var request = new UpdateProductRequest
            {
                Id = 1,
                Title = "New Title",
                Price = 100,
                Description = "New Description",
                Category = "New Category",
                Image = "new-image-url",
                Rating = new RatingDTO { Rate = 5, Count = 10 }
            };

            var product = new Product(1, "title", 10, "desc", "category", "http://image.com", new Rating(1, 2));

            var updatedProduct = new Product(1, "New Title", 100, "New Description", "New Category", "new-image-url", new Rating(5, 10));

            var response = new UpdateProductResponse
            {
                Id = 1,
                Title = "New Title",
                Price = 100,
                Description = "New Description",
                Category = "New Category",
                Image = "new-image-url",
                Rating = new UpdateProductRatingResponse() { Rate = 5, Count = 10 }
            };

            _productRepository.Get(request.Id, Arg.Any<CancellationToken>()).Returns(product);
            _productRepository.Update(product);//.Returns(updatedProduct);
            _mapper.Map<UpdateProductResponse>(updatedProduct).Returns(response);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            // Verificando se os valores foram atualizados corretamente no objeto product
            Assert.Equal("New Title", product.Title);
            Assert.Equal(100, product.Price);
            Assert.Equal("New Description", product.Description);
            Assert.Equal("New Category", product.Category);
            Assert.Equal("new-image-url", product.Image);
            Assert.Equal(5, product.Rating.Rate);
            Assert.Equal(10, product.Rating.Count);


            // Verificando se os métodos do repositório e UnitOfWork foram chamados
            await _productRepository.Received(1).Get(request.Id, Arg.Any<CancellationToken>());
            await _unitOfWork.Received(1).Commit(Arg.Any<CancellationToken>());
        }
    }
}

