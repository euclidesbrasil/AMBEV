using Ambev.Core.Application.UseCases.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.Core.Application.UseCases.Commands.Sale.UpdateSale
{
    public class UpdateSaleRequest : SaleDTO, IRequest<UpdateSaleResponse>
    {
        public List<SaleItemDTO> Itens { get; set; }
    }
}
