using Ambev.Core.Application.UseCases.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.Core.Application.UseCases.Commands.Branch.UpdateBranch
{
    public class UpdateBranchRequest : BranchBaseDTO, IRequest<UpdateBranchResponse>
    {
        public int Id { get; internal set; }

        public void UpdateBranchRequestIdContext(int id)
        {
            Id = id;
        }
    }
}
