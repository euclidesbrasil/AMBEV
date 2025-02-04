using Ambev.DeveloperEvaluation.Common.EventBroker;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Integration.Domain.Services
{
    /// <summary>
    /// Contains integration tests for StockServices.
    /// Tests cover availabilty of products.
    /// </summary>
    public class StockServiceTest
    {
        private readonly IStockRepository _stockRepository;
        private readonly IStockService _stockService;

        public StockServiceTest()
        {
            _stockRepository = Substitute.For<IStockRepository>();
            _stockService = new StockService(_stockRepository, Substitute.For<IEventBroker>());
        }

        /// <summary>
        /// Tests the return of a product availabilty on a sale.
        /// </summary>
        [Fact(DisplayName = "Given a sale then should verify the stock available")]
        public async Task Given_Sale_WhenProductIsAvailable_Then_ShouldVerifyStockAndAllowsSale()
        {
            // Given
            var stock = new Stock
            {
                AvailableQuantity = 10,
                ProductId = Guid.NewGuid(),
                BranchId = Guid.NewGuid()
            };

            _stockRepository.GetByBranchAndProductAsync(Arg.Any<Guid>(), Arg.Any<Guid>()).Returns(stock);

            // When
            var result = await _stockService.CheckProductAvailabilty(
                Guid.NewGuid(), 5, Guid.NewGuid(), CancellationToken.None);

            // Then
            Assert.True(result);
        }

        /// <summary>
        /// Tests the return of a product out of stock.
        /// </summary>
        [Fact(DisplayName = "Given a sale then should verify the stock available")]
        public async Task Given_Sale_WhenProductIsUnavailable_Then_ShouldItemBeCanceled()
        {
            // Given
            var stock = new Stock
            {
                AvailableQuantity = 5,
                ProductId = Guid.NewGuid(),
                BranchId = Guid.NewGuid()
            };

            _stockRepository.GetByBranchAndProductAsync(Arg.Any<Guid>(), Arg.Any<Guid>()).Returns(stock);

            // When
            var result = await _stockService.CheckProductAvailabilty(
                Guid.NewGuid(), 10, Guid.NewGuid(), CancellationToken.None);

            // Then
            Assert.False(result);
        }
    }
}
