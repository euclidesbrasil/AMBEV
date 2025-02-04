using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData
{
    /// <summary>
    /// Provides methods for generating test data using the Bogus library for the Sale entity
    /// </summary>
    public static class SaleTestData
    {
        /// <summary>
        /// Generate a Sale object
        /// </summary>
        /// <returns>A valid Sale entity with randomly generated data.</returns>
        public static Sale GenerateSale()
        {
            var sale = SaleFaker.Generate();
            sale.Items = GenerateValidListOfItems();

            return sale;
        }

        public static SaleItem GenerateSaleItem()
        {
            return SaleItemFaker.Generate();
        }

        /// <summary>
        /// Generate a list of items to be used on SaleFaker
        /// </summary>
        /// <returns>A List of item entity with randomly generated data.</returns>
        public static List<SaleItem> GenerateValidListOfItems()
        {
            return SaleItemFaker.GenerateBetween(1, 10);
        }

        /// <summary>
        /// Configures the Faker to generate SaleItem for use on SaleTestData
        /// </summary>
        private static readonly Faker<Sale> SaleFaker = new Faker<Sale>()
            .RuleFor(u => u.Id, f => f.Random.Guid())
            .RuleFor(u => u.Number, f => f.Random.Number(1, 100))
            .RuleFor(u => u.BranchId, f => f.Random.Guid())
            .RuleFor(u => u.CustomerId, f => f.Random.Guid())
            .RuleFor(u => u.Status, f => SaleStatus.Pending)
            .RuleFor(u => u.CreatedAt, f => DateTime.Now)
            .RuleFor(u => u.UpdatedAt, f => DateTime.Now);

        /// <summary>
        /// Configures the Faker to generate SaleItem for use on SaleTestData
        /// </summary>
        private static readonly Faker<SaleItem> SaleItemFaker = new Faker<SaleItem>()
            .RuleFor(u => u.ProductId, f => f.Random.Uuid())
            .RuleFor(u => u.Quantity, f => f.Random.Number(1, 100))
            .RuleFor(u => u.Price, f => f.Random.Decimal())
            .RuleFor(u => u.Canceled, false);
    }
}
