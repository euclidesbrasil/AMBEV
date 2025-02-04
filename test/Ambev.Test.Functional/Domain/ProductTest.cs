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
    public class ProductTests
    {
        [Fact]
        public void Constructor_Should_SetPropertiesCorrectly()
        {
            // Arrange
            var rating = new Rating(1,2);

            // Act
            var product = new Product("Product Title", 99.99m, "Product Description", "Category", "ImageURL", rating);

            // Assert
            Assert.Equal("Product Title", product.Title);
            Assert.Equal(99.99m, product.Price);
            Assert.Equal("Product Description", product.Description);
            Assert.Equal("Category", product.Category);
            Assert.Equal("ImageURL", product.Image);
            Assert.Equal(rating, product.Rating);
        }

        [Fact]
        public void Update_Should_UpdatePropertiesCorrectly()
        {
            // Arrange
            var product = new Product("Product Title", 99.99m, "Product Description", "Category", "ImageURL", new Rating(1, 2));
            var newRating = new Rating(1,2);

            // Act
            product.Update("Updated Title", 89.99m, "Updated Description", "Updated Category", "Updated ImageURL", newRating);

            // Assert
            Assert.Equal("Updated Title", product.Title);
            Assert.Equal(89.99m, product.Price);
            Assert.Equal("Updated Description", product.Description);
            Assert.Equal("Updated Category", product.Category);
            Assert.Equal("Updated ImageURL", product.Image);
            Assert.Equal(newRating, product.Rating);
        }

        [Fact]
        public void Constructor_WithId_Should_SetPropertiesCorrectly()
        {
            // Arrange
            var rating = new Rating(1,2);

            // Act
            var product = new Product(1, "Product Title", 99.99m, "Product Description", "Category", "ImageURL", rating);

            // Assert
            Assert.Equal(1, product.Id);
            Assert.Equal("Product Title", product.Title);
            Assert.Equal(99.99m, product.Price);
            Assert.Equal("Product Description", product.Description);
            Assert.Equal("Category", product.Category);
            Assert.Equal("ImageURL", product.Image);
            Assert.Equal(rating, product.Rating);
        }
    }
}
