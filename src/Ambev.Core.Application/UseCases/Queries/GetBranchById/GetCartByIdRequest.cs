using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.Core.Application.Branch.GetBranchById.GetBranchById
{
    public sealed record GetBranchByIdRequest(int id) : IRequest<GetBranchByIdResponse>;
}
