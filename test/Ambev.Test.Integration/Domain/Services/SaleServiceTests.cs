using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;
using Ambev.DeveloperEvaluation.Integration.Domain.Services.TestData;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Integration.Domain.Services
{
    public class SaleServiceTests
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IBranchRepository _branchRepository;
        private readonly IProductRepository _productRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly SaleService _saleService;

        public SaleServiceTests()
        {
            _saleRepository = Substitute.For<ISaleRepository>();
            _branchRepository = Substitute.For<IBranchRepository>();
            _productRepository = Substitute.For<IProductRepository>();
            _customerRepository = Substitute.For<ICustomerRepository>();
            _saleService = new SaleService(_saleRepository, _customerRepository, _branchRepository, _productRepository);
        }

        /// <summary>
        /// Tests the return of a sale.
        /// </summary>
        [Fact(DisplayName = "Given a id of a sale then should return the sale complete information")]
        public async Task Given_SaleId_Then_ShouldReturnSaleCompleteInformation()
        {
            // Given
            var sale = SaleManagerServiceTestData.GenerateSale();
            _saleRepository.GetWithItemsByIdAsync(Arg.Any<Guid>()).Returns(sale);
            _branchRepository.GetByIdAsync(Arg.Any<Guid>()).Returns(new Branch());
            _customerRepository.GetByIdAsync(Arg.Any<Guid>()).Returns(new Customer());
            _productRepository.GetByIdAsync(Arg.Any<Guid>()).Returns(SaleManagerServiceTestData.GenerateProduct());

            // When
            var result = await _saleService.GetSale(Guid.NewGuid(), CancellationToken.None);

            // Then
            Assert.NotNull(result);
        }
    }
}
