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
    public class CustomerTests
    {
        [Fact]
        public void Constructor_Should_SetPropertiesCorrectly()
        {
            // Arrange & Act
            var customer = new Customer { FirstName = "John", LastName = "Doe", Identification = "123456789" };

            // Assert
            Assert.Equal("John", customer.FirstName);
            Assert.Equal("Doe", customer.LastName);
            Assert.Equal("123456789", customer.Identification);
        }

        [Fact]
        public void Update_Should_UpdatePropertiesCorrectly()
        {
            // Arrange
            var customer = new Customer { FirstName = "John", LastName = "Doe", Identification = "123456789" };

            // Act
            customer.Update("Jane", "Smith", "987654321");

            // Assert
            Assert.Equal("Jane", customer.FirstName);
            Assert.Equal("Smith", customer.LastName);
            Assert.Equal("987654321", customer.Identification);
        }
    }
}
