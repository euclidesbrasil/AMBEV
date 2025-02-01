using Ambev.Core.Application.UseCases.Queries.GetSaleById;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.Core.Application.UseCases.Queries.GetSalesQuery
{
    public sealed record GetSalesQueryRequest(int page, int size, string order) : IRequest<GetSalesQueryResponse>;
}
