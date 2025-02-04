using Ambev.Core.Domain.Entities;
using Xunit;

namespace Ambev.Test.Domain
{
    public class SaleItemTests
    {
        [Fact]
        public void TotalPrice_Should_CalculateCorrectly_When_NotCancelled()
        {
            // Arrange
            var saleItem = new SaleItem(1, 1, "Product1", 5, 10.0m, false);

            // Act
            var totalPrice = saleItem.TotalPrice;

            // Assert
            Assert.Equal(50.0m, totalPrice);
        }

        [Fact]
        public void TotalPrice_Should_BeZero_When_Cancelled()
        {
            // Arrange
            var saleItem = new SaleItem(1, 1, "Product1", 5, 10.0m, true);

            // Act
            var totalPrice = saleItem.TotalPrice;

            // Assert
            Assert.Equal(0.0m, totalPrice);
        }

        [Fact]
        public void ApplyDiscount_Should_SetDiscount_Correctly()
        {
            // Arrange
            var saleItem = new SaleItem(1, 1, "Product1", 5, 10.0m, false);

            // Act
            saleItem.ApplyDiscount();

            // Assert
            Assert.Equal(5.0m, saleItem.Discount);
            Assert.Equal(45.0m, saleItem.TotalPrice);
        }

        [Fact]
        public void VerifyAllowedQuantity_Should_ThrowException_When_QuantityExceedsLimit()
        {
            // Arrange
            var saleItem = new SaleItem(1, 1, "Product1", 25, 10.0m, false);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => saleItem.VerifyAllowedQuantity());
        }

        [Fact]
        public void CreateItemCanceledEvent_Should_ReturnEvent_When_ItemIsCancelled()
        {
            // Arrange
            var saleItem = new SaleItem(1, 1, "Product1", 5, 10.0m, true);

            // Act
            var eventResult = saleItem.CreateItemCanceledEvent(false);

            // Assert
            Assert.NotNull(eventResult);
            Assert.Equal("Item canceled", eventResult.Message);
        }

        [Fact]
        public void CreateItemCanceledEvent_Should_ReturnNull_When_ItemIsNotCancelled()
        {
            // Arrange
            var saleItem = new SaleItem(1, 1, "Product1", 5, 10.0m, false);

            // Act
            var eventResult = saleItem.CreateItemCanceledEvent(false);

            // Assert
            Assert.Null(eventResult);
        }
    }
}
