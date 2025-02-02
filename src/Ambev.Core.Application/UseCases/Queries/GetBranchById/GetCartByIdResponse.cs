using Ambev.Core.Application.UseCases.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.Core.Application.Branch.GetBranchById.GetBranchById
{
    public class GetBranchByIdResponse : BranchBaseDTO
    {
        public int Id { get; init; }
    }
}
