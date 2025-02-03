using AutoMapper;
using Ambev.Core.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Ambev.Core.Application.Branch.GetBranchById.GetBranchById;

namespace Ambev.Core.Application.UseCases.Branch.GetBranchById
{
    public class GetBranchByIdHandler : IRequestHandler<GetBranchByIdRequest, GetBranchByIdResponse>
    {
        private readonly IBranchRepository _branchRepository;
        private readonly IMapper _mapper;

        public GetBranchByIdHandler(IBranchRepository branchRepository, IMapper mapper)
        {
            _branchRepository = branchRepository;
            _mapper = mapper;
        }

        public async Task<GetBranchByIdResponse> Handle(GetBranchByIdRequest request, CancellationToken cancellationToken)
        {
            var branch = await _branchRepository.Get(request.id, cancellationToken);
            
            if(branch is null)
            {
                throw new KeyNotFoundException($"Branch with ID  {request.id} does not exist in our database");
            }

            return _mapper.Map<GetBranchByIdResponse>(branch);
        }
    }
}
