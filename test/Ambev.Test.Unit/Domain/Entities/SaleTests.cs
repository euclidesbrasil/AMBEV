using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities
{
    public class SaleTests
    {
        /// <summary>
        /// Tests the status when a sales is created
        /// </summary>
        [Fact(DisplayName = "Sales status should change to Approved")]
        public void Given_SaleWhenApproved_Then_StatusShouldBeApproved()
        {
            var sale = SaleTestData.GenerateSale();

            sale.Approve();

            Assert.Equal(SaleStatus.Approved, sale.Status);
            Assert.NotEqual(DateTime.MinValue, sale.UpdatedAt);
            Assert.NotEqual(sale.CreatedAt, sale.UpdatedAt);
        }

        /// <summary>
        /// Tests that when a sales is canceled by the Customer
        /// </summary>
        [Fact(DisplayName = "Sales status should change from Approved to Canceled")]
        public void Given_CanceledSale_When_Canceled_Then_StatusShouldBeCanceled()
        {
            var sale = SaleTestData.GenerateSale();

            sale.Cancel();

            Assert.Equal(SaleStatus.Canceled, sale.Status);
            Assert.NotEqual(DateTime.MinValue, sale.UpdatedAt);
            Assert.NotEqual(sale.CreatedAt, sale.UpdatedAt);
        }

        /// <summary>
        /// Tests the generation of the Sale Number
        /// </summary>
        [Fact(DisplayName = "Sale should generate the Number when provided a current sale number")]
        public void Given_Sale_WhenGenerateNumber_Then_TheNumberHasToBeDifferentOfTheCurrent()
        {
            var sale = SaleTestData.GenerateSale();

            sale.GenerateSaleNumber(1);

            Assert.Equal(SaleStatus.Pending, sale.Status);
            Assert.NotEqual(DateTime.MinValue, sale.UpdatedAt);
            Assert.NotEqual(DateTime.MinValue, sale.CreatedAt);
        }

        /// <summary>
        /// Tests the generation of the Sale Number
        /// </summary>
        [Fact(DisplayName = "Validation should pass for valid sale data")]
        public void Given_ValidSaleData_When_Validated_Then_ShouldReturnValid()
        {
            var sale = SaleTestData.GenerateSale();

            var result = sale.Validate();

            Assert.True(result.IsValid);
            Assert.Empty(result.Errors);
        }

        /// <summary>
        /// Tests the update of the Sale
        /// </summary>
        [Fact(DisplayName = "Should allow update sale")]
        public void Given_ValidSaleData_When_Update_Then_ShouldReturnValidSale()
        {
            var sale = SaleTestData.GenerateSale();
            sale.Update(SaleTestData.GenerateValidListOfItems(), false);

            Assert.NotEqual(SaleStatus.Canceled, sale.Status);
        }

        /// <summary>
        /// Tests the cancel of a Sale
        /// </summary>
        [Fact(DisplayName = "Cancel a sale should change the status")]
        public void Given_ValidSaleData_When_Cancel_Then_ShouldChangeTheStatusToCanceled()
        {
            var sale = SaleTestData.GenerateSale();
            sale.Update(SaleTestData.GenerateValidListOfItems(), true);

            Assert.Equal(SaleStatus.Canceled, sale.Status);
        }
    }
}
