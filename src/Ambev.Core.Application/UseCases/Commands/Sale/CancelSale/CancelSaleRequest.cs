using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.Core.Application.UseCases.Commands.Sale.CancelSale
{
    public sealed record CancelSaleRequest(int id): IRequest<CancelSaleResponse>
    {
    }
}
