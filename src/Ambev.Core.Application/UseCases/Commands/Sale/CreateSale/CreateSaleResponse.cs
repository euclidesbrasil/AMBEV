using Ambev.Core.Application.UseCases.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.Core.Application.UseCases.Commands.Sale.CreateSale
{
    public class CreateSaleResponse: SaleDTO
    {
        public string UserFirstName { get; set; } // Nome do Cliente (desnormalizado)
        public string BranchName { get; set; } // Nome da Filial (desnormalizado)
    }

    public class CreateSaleResponseItem : SaleItemDTO
    {
        public string ProductName { get; set; } // Nome do Produto (desnormalizado)
    }
}
