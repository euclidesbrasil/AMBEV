using System;
using System.Collections.Generic;
using Xunit;
using Ambev.Core.Domain.Entities;
namespace Ambev.Test.Domain
{
    public class SaleTests
    {
        [Fact]
        public void TotalAmount_Should_CalculateCorrectly()
        {
            // Arrange
            var items = new List<SaleItem>
        {
            new SaleItem { UnitPrice = 10.0m, Quantity = 2, IsCancelled = false },
            new SaleItem { UnitPrice = 5.0m, Quantity = 3, IsCancelled = false }
        };
            var sale = new Sale { Items = items };

            // Act
            var totalAmount = sale.TotalAmount;

            // Assert
            Assert.Equal(35.0m, totalAmount);
        }

        [Fact]
        public void Cancel_Should_SetIsCancelled_And_CancelAllItems()
        {
            // Arrange
            var items = new List<SaleItem>
        {
            new SaleItem { IsCancelled = false },
            new SaleItem { IsCancelled = false }
        };
            var sale = new Sale { Items = items };

            // Act
            sale.Cancel();

            // Assert
            Assert.True(sale.IsCancelled);
            Assert.All(sale.Items, item => Assert.True(item.IsCancelled));
        }

        [Fact]
        public void Update_Should_UpdateFields_And_CancelIfNeeded()
        {
            // Arrange
            var customer = new Customer { Id = 1, FirstName = "John", LastName = "Doe" };
            var sale = new Sale { IsCancelled = false, Items = new List<SaleItem> { new SaleItem { IsCancelled = false } } };
            var request = new Sale
            {
                SaleDate = DateTime.Now,
                BranchId = 2,
                IsCancelled = true
            };
            request.GenerateSaleNumber();

            // Act
            sale.Update(request, customer);

            // Assert
            Assert.Equal(2, sale.BranchId);
            Assert.True(sale.IsCancelled);
            Assert.Equal(customer.Id, sale.CustomerId);
            Assert.Equal(customer.FirstName, sale.CustomerFirstName);
            Assert.Equal(customer.LastName, sale.CustomerLastName);
            Assert.All(sale.Items, item => Assert.True(item.IsCancelled));
        }

        [Fact]
        public void ClearItems_Should_ClearItemsList()
        {
            // Arrange
            var items = new List<SaleItem> { new SaleItem(), new SaleItem() };
            var sale = new Sale { Items = items };

            // Act
            sale.ClearItems();

            // Assert
            Assert.Empty(sale.Items);
        }

        [Fact]
        public async Task ApplyBusinessRules_Should_ThrowException_When_TotalQuantityExceedsLimit()
        {
            // Arrange
            var items = new List<SaleItem>
        {
            new SaleItem { ProductId = 1, Quantity = 15, IsCancelled = false },
            new SaleItem { ProductId = 1, Quantity = 10, IsCancelled = false }
        };
            var sale = new Sale { Items = items };
            try
            {
                sale.ApplyBusinessRules();
                Assert.True(false);
            }
            catch (InvalidOperationException ex)
            {
                Assert.True(ex.Message.Contains("The limit per item is 20 units"));
            }
        }

        [Fact]
        public void GetSaleCanceledEvent_Should_ReturnEvent_When_SaleIsCancelled()
        {
            // Arrange
            var sale = new Sale { IsCancelled = true };

            // Act
            var eventResult = sale.GetSaleCanceledEvent();

            // Assert
            Assert.NotNull(eventResult);
            Assert.Equal("Sale canceled", eventResult.Message);
        }

        [Fact]
        public void GetSaleCanceledEvent_Should_ReturnNull_When_SaleIsNotCancelled()
        {
            // Arrange
            var sale = new Sale { IsCancelled = false };

            // Act
            var eventResult = sale.GetSaleCanceledEvent();

            // Assert
            Assert.Null(eventResult);
        }
    }
}
