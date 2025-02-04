using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

namespace Ambev.DeveloperEvaluation.Integration.Application.TestData
{
    public static class CreateSaleHandlerTestData
    {
        public static CreateSaleCommand CreateValidCommand()
        {
            return new CreateSaleCommand()
            {
                BranchId = Guid.NewGuid(),
                CustomerId = Guid.NewGuid(),
                Items = new List<CreateSaleItemCommand>()
                {
                    new CreateSaleItemCommand() 
                    {
                        ProductId = Guid.NewGuid(),
                        Quantity = 10
                    }
                }
            };
        }

        public static UpdateSaleCommand CreateValidUpdateCommand()
        {
            return new UpdateSaleCommand()
            {
                SaleId = Guid.NewGuid(),
                Items = new List<UpdateSaleItemCommand>()
                {
                    new UpdateSaleItemCommand()
                    {
                        ProductId = Guid.NewGuid(),
                        Quantity = 10,
                        Canceled = true
                    }
                }
            };
        }

        public static UpdateSaleCommand CreateInvalidUpdateCommand()
        {
            return new UpdateSaleCommand()
            {
                SaleId = Guid.Empty,
                Items = new List<UpdateSaleItemCommand>()
            };
        }

        public static CreateSaleCommand CreateInvalidCommand()
        {
            return new CreateSaleCommand()
            {
                BranchId = Guid.Empty,
                CustomerId = Guid.Empty,
                Items = new List<CreateSaleItemCommand>()
            };
        }
    }
}
