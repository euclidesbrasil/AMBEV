using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Validation;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Validation
{
    public class SaleValidatorTests
    {
        private readonly SaleValidator _validator;

        public SaleValidatorTests()
        {
            _validator = new SaleValidator();
        }

        /// <summary>
        /// Tests that validation passes when all sale properties are valid.
        /// </summary>
        [Fact(DisplayName = "Valid sale should pass all validation rules")]
        public void Given_ValidSale_When_Validated_Then_ShouldNotHaveErrors()
        {
            // Arrange
            var sale = SaleTestData.GenerateSale();

            // Act
            var result = _validator.TestValidate(sale);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        /// <summary>
        /// Tests that validation fails for invalid number
        /// </summary>
        [Fact(DisplayName = "Invalid sale number should fail validation")]
        public void Given_InvalidNumber_When_Validated_Then_ShouldHaveError()
        {
            // Arrange
            var sale = SaleTestData.GenerateSale();
            sale.Number = 0;

            // Act
            var result = _validator.TestValidate(sale);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Number);
        }

        /// <summary>
        /// Tests that validation fails for invalid list of items
        /// </summary>
        [Fact(DisplayName = "Invalid sale items should fail validation")]
        public void Given_InvalidListOfItems_When_Validated_Then_ShouldHaveError()
        {
            // Arrange
            var sale = SaleTestData.GenerateSale();
            sale.Items = new List<SaleItem>();

            // Act
            var result = _validator.TestValidate(sale);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Items);
        }

        /// <summary>
        /// Tests that validation fails for invalid customerId
        /// </summary>
        [Fact(DisplayName = "Invalid customerId should fail validation")]
        public void Given_InvalidCustomerId_When_Validated_Then_ShouldHaveError()
        {
            // Arrange
            var sale = SaleTestData.GenerateSale();
            sale.CustomerId = Guid.Empty;

            // Act
            var result = _validator.TestValidate(sale);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.CustomerId);
        }

        /// <summary>
        /// Tests that validation fails for invalid branchId
        /// </summary>
        [Fact(DisplayName = "Invalid branchId should fail validation")]
        public void Given_InvalidBranchId_When_Validated_Then_ShouldHaveError()
        {
            // Arrange
            var sale = SaleTestData.GenerateSale();
            sale.BranchId = Guid.Empty;

            // Act
            var result = _validator.TestValidate(sale);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.BranchId);
        }
    }
}
