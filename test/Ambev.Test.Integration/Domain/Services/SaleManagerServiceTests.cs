using Ambev.DeveloperEvaluation.Common.EventBroker;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;
using Ambev.DeveloperEvaluation.Integration.Domain.Services.TestData;
using FluentValidation;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Integration.Domain.Services
{
    public class SaleManagerServiceTests
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IProductRepository _productRepository;
        private readonly IStockService _stockService;
        private readonly SaleManagerService _saleService;

        public SaleManagerServiceTests()
        {
            _saleRepository = Substitute.For<ISaleRepository>();
            _productRepository = Substitute.For<IProductRepository>();
            _stockService = Substitute.For<IStockService>();
            _saleService = new SaleManagerService(_saleRepository, _productRepository, _stockService, Substitute.For<IEventBroker>());
        }

        /// <summary>
        /// Tests the creation of a valid sale.
        /// </summary>
        [Fact(DisplayName = "Given valid sale then should return the sale created")]
        public async Task Given_ValidSaleRequest_Then_ShouldReturnTheCreatedSale()
        {
            // Given
            var sale = SaleManagerServiceTestData.GenerateSale();
            var items = SaleManagerServiceTestData.GenerateValidListOfItems();
            sale.Items = items;

            _saleRepository.GetLastSaleNumber().Returns(1);
            _saleRepository.CreateAsync(Arg.Any<Sale>()).Returns(sale);
            _productRepository.GetByIdAsync(Arg.Any<Guid>()).Returns(SaleManagerServiceTestData.GenerateProduct());
            _stockService.CheckProductAvailabilty(Arg.Any<Guid>(), Arg.Any<int>(), Arg.Any<Guid>()).Returns(true);

            // When
            var result = await _saleService.CreateSale(sale, CancellationToken.None);

            // Then
            Assert.NotNull(result);
            Assert.Equal(SaleStatus.Approved, result.Status);
            Assert.NotEqual(0, result.GetTotalSaleAmount());
        }

        /// <summary>
        /// Tests the creation of a sale with invalid data
        /// </summary>
        [Fact(DisplayName = "Given invalid sale then should return errors")]
        public async Task Given_InvalidSale_Then_ShouldThrowErrors()
        {
            // Given
            var sale = SaleManagerServiceTestData.GenerateInvalidSale();
            _saleRepository.GetLastSaleNumber().Returns(1);

            // When
            var act = () => _saleService.CreateSale(sale, CancellationToken.None);

            // Then
            await Assert.ThrowsAsync<ValidationException>(act);
        }

        /// <summary>
        /// Tests the update of a existent sale
        /// </summary>
        [Fact(DisplayName = "Given valid updated sale then should return the sale created")]
        public async Task Given_ValidUpdatedSale_Then_ShouldReturnSale()
        {
            // Given
            var sale = SaleManagerServiceTestData.GenerateSale();
            var items = SaleManagerServiceTestData.GenerateValidListOfItems();
            
            sale.Items = items;
            sale.Update(items, false);

            _saleRepository.UpdateAsync(Arg.Any<Sale>()).Returns(sale);
            _productRepository.GetByIdAsync(Arg.Any<Guid>()).Returns(SaleManagerServiceTestData.GenerateProduct());
            _stockService.CheckProductAvailabilty(Arg.Any<Guid>(), Arg.Any<int>(), Arg.Any<Guid>()).Returns(true);

            // When
            var result = await _saleService.UpdateSale(sale, CancellationToken.None);

            // Then
            Assert.NotNull(result);
            Assert.Equal(SaleStatus.Approved, result.Status);
            Assert.NotEqual(0, result.GetTotalSaleAmount());
        }

        /// <summary>
        /// Tests the creation of a valid sale with products out of stock
        /// </summary>
        [Fact(DisplayName = "Given valid sale when stock is unavailable then should return the sale created")]
        public async Task Given_ValidSaleRequest_WhenStockIsUnavailable_Then_ShouldCancelTheItems()
        {
            // Given
            var sale = SaleManagerServiceTestData.GenerateSale();
            var items = SaleManagerServiceTestData.GenerateValidListOfItems();
            sale.Items = items;

            _saleRepository.GetLastSaleNumber().Returns(1);
            _saleRepository.CreateAsync(Arg.Any<Sale>()).Returns(sale);
            _productRepository.GetByIdAsync(Arg.Any<Guid>()).Returns(SaleManagerServiceTestData.GenerateProduct());
            _stockService.CheckProductAvailabilty(Arg.Any<Guid>(), Arg.Any<int>(), Arg.Any<Guid>()).Returns(false);

            // When
            var result = await _saleService.CreateSale(sale, CancellationToken.None);

            // Then
            Assert.NotNull(result);
            Assert.Equal(0, result.GetTotalSaleAmount());
        }
    }
}
