using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Services;
using Ambev.DeveloperEvaluation.Integration.Application.TestData;
using Ambev.DeveloperEvaluation.Integration.Domain.Services.TestData;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Integration.Application
{
    /// <summary>
    /// Contains unit tests for the <see cref="CreateSaleHandler"/> class.
    /// </summary>
    public class CreateSaleHandlerTests
    {
        private readonly ISaleManagerService _saleService;
        private readonly IMapper _mapper;
        private readonly CreateSaleHandler _handler;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateSaleHandlerTests"/> class.
        /// Sets up the test dependencies and creates fake data generators.
        /// </summary>
        public CreateSaleHandlerTests()
        {
            _saleService = Substitute.For<ISaleManagerService>();
            _mapper = Substitute.For<IMapper>();
            _handler = new CreateSaleHandler(_saleService, _mapper);
        }

        /// <summary>
        /// Tests that a valid sale creation request is handled successfully.
        /// </summary>
        [Fact(DisplayName = "Given valid sale data When creating sale Then returns success response")]
        public async Task Handle_ValidRequest_ReturnsSuccessResponse()
        {
            // Given
            var command = CreateSaleHandlerTestData.CreateValidCommand();
            var sale = SaleManagerServiceTestData.GenerateSale();
            _saleService.CreateSale(Arg.Any<Sale>()).Returns(sale);

            var result = new CreateSaleResult
            {
                CreatedAt = DateTime.UtcNow,
                Number = 1
            };

            _mapper.Map<Sale>(command).Returns(sale);
            _mapper.Map<CreateSaleResult>(sale).Returns(result);

            // When
            var createSaleResult = await _handler.Handle(command, CancellationToken.None);

            // Then
            createSaleResult.Should().NotBeNull();
            createSaleResult.Number.Should().Be(1);
            await _saleService.Received(1).CreateSale(Arg.Any<Sale>(), Arg.Any<CancellationToken>());
        }

        /// <summary>
        /// Tests that an invalid sale creation request is handled
        /// </summary>
        [Fact(DisplayName = "Given an invalid sale data When creating sale Then returns exception")]
        public async Task Handle_InvalidRequest_ReturnsException()
        {
            // Given
            var command = CreateSaleHandlerTestData.CreateInvalidCommand();

            // When
            var act = () => _handler.Handle(command, CancellationToken.None);

            // Then
            await act.Should().ThrowAsync<FluentValidation.ValidationException>();
        }
    }
}
