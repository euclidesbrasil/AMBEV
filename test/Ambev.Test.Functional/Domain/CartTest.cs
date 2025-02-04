using System;
using System.Collections.Generic;
using Ambev.Core.Domain.Entities;
using Ambev.Core.Domain.Enum;
using Ambev.Core.Domain.Interfaces;
using Ambev.Core.Domain.ValueObjects;
using NSubstitute;
using Xunit;
namespace Ambev.Test.Domain
{
    public class CartTests
    {
        [Fact]
        public void Constructor_Should_SetPropertiesCorrectly()
        {
            // Arrange
            var date = DateTime.Now;
            var products = new List<CartItem>
        {
            new CartItem(1,2,10.0m),
            new CartItem(2,1,20.0m)
        };

            // Act
            var cart = new Cart(1, date, products);

            // Assert
            Assert.Equal(1, cart.UserId);
            Assert.Equal(date, cart.Date);
            Assert.Equal(2, cart.Products.Count);
            Assert.Equal(1, cart.Products[0].ProductId);
            Assert.Equal(2, cart.Products[0].Quantity);
            Assert.Equal(10.0m, cart.Products[0].Price);
            Assert.Equal(2, cart.Products[1].ProductId);
            Assert.Equal(1, cart.Products[1].Quantity);
            Assert.Equal(20.0m, cart.Products[1].Price);
        }

        [Fact]
        public void AddProducts_Should_AddNewProductsCorrectly()
        {
            // Arrange
            var cart = new Cart(1, DateTime.Now, new List<CartItem>() { new CartItem(1, 2, 10.0m) });

            // Assert
            Assert.Single(cart.Products);
            Assert.Equal(1, cart.Products[0].ProductId);
            Assert.Equal(2, cart.Products[0].Quantity);
            Assert.Equal(10.0m, cart.Products[0].Price);
        }

        [Fact]
        public void AddProducts_Should_UpdateQuantityIfExists()
        {
            // Arrange
            var products = new List<CartItem>
        {
            new CartItem(1,2,10.0m)
        };
            var cart = new Cart(1, DateTime.Now, products);
            var newProducts = new List<CartItem>
        {
            new CartItem(1,3,10.0m)
        };

            // Act
            cart.AddProducts(newProducts);

            // Assert
            Assert.Single(cart.Products);
            Assert.Equal(1, cart.Products[0].ProductId);
            Assert.Equal(5, cart.Products[0].Quantity);
        }

        [Fact]
        public void RemoveProduct_Should_RemoveProductCorrectly()
        {
            // Arrange
            var products = new List<CartItem>
        {
            new CartItem(1,2,10.0m)
        };
            var cart = new Cart(1, DateTime.Now, products);

            // Act
            cart.RemoveProduct(1);

            // Assert
            Assert.Empty(cart.Products);
        }

        [Fact]
        public void RemoveProduct_Should_ThrowException_When_ProductNotFound()
        {
            // A lista de produtos não pode ser vazia.
            try
            {
                // Arrange
                var cart = new Cart(1, DateTime.Now, new List<CartItem>());

                Assert.True(false);
            }
            catch (ArgumentException ex)
            {
                Assert.True("A lista de produtos não pode ser vazia." == ex.Message);
            }
        }

        [Fact]
        public void UpdateProductQuantity_Should_UpdateQuantityCorrectly()
        {
            // Arrange
            var products = new List<CartItem>
        {
            new CartItem(1,5,10.0m)
        };
            var cart = new Cart(1, DateTime.Now, products);

            // Act
            cart.UpdateProductQuantity(1, 5);

            // Assert
            Assert.Single(cart.Products);
            Assert.Equal(1, cart.Products[0].ProductId);
            Assert.Equal(5, cart.Products[0].Quantity);
        }

        [Fact]
        public void UpdateProductQuantity_Should_ThrowException_When_ProductNotFound()
        {
            // Arrange
            var cart = new Cart(1, DateTime.Now, new List<CartItem>() { new CartItem(9, 9) });

            // Act & Assert
            Assert.Throws<ArgumentException>(() => cart.UpdateProductQuantity(1, 5));
        }

        [Fact]
        public void CalculateTotal_Should_ReturnCorrectTotal()
        {
            // Arrange
            var products = new List<CartItem>
        {
            new CartItem(1,2,10.0m),
            new CartItem(2,1,10.0m)
        };
            var cart = new Cart(1, DateTime.Now, products);

            // Act
            var total = cart.CalculateTotal();

            // Assert
            Assert.Equal(30.0m, total);
        }

        [Fact]
        public void RemoveAllProducts_Should_ClearAllProducts()
        {
            // Arrange
            var products = new List<CartItem>
        {
            new CartItem(1,2,10.0m),
            new CartItem(2,1,10.0m)
        };
            var cart = new Cart(1, DateTime.Now, products);

            // Act
            cart.RemoveAllProducts();

            // Assert
            Assert.Empty(cart.Products);
        }
    }
}
