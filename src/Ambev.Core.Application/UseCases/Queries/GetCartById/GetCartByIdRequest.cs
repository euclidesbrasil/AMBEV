using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.Core.Application.UseCases.Queries.GetCartQueryId
{
    public sealed record GetCartByIdRequest(int id) : IRequest<GetCartByIdResponse>;
}
