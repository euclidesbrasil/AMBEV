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
    public class BranchTests
    {
        [Fact]
        public void Constructor_Should_SetPropertiesCorrectly()
        {
            // Arrange & Act
            var branch = new Branch
            {
                Id = 1,
                Name = "Branch Name",
                Location = "Branch Location"
            };

            // Assert
            Assert.Equal(1, branch.Id);
            Assert.Equal("Branch Name", branch.Name);
            Assert.Equal("Branch Location", branch.Location);
        }

        [Fact]
        public void Update_Should_UpdatePropertiesCorrectly()
        {
            // Arrange
            var branch = new Branch
            {
                Id = 1,
                Name = "Old Name",
                Location = "Old Location"
            };

            // Act
            branch.Update("New Name", "New Location");

            // Assert
            Assert.Equal("New Name", branch.Name);
            Assert.Equal("New Location", branch.Location);
        }
    }
}