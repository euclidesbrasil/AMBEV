using Ambev.Core.Domain.Interfaces;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities = Ambev.Core.Domain.Entities;
namespace Ambev.Core.Application.UseCases.Commands.Branch.UpdateBranch
{
    public class UpdateBranchHandler :
       IRequestHandler<UpdateBranchRequest, UpdateBranchResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBranchRepository _branchRepository;
        private readonly IMapper _mapper;

        public UpdateBranchHandler(IUnitOfWork unitOfWork,
            IBranchRepository branchRepository,
            IMapper mapper
            )
        {
            _unitOfWork = unitOfWork;
            _branchRepository = branchRepository;
            _mapper = mapper;
        }

        public async Task<UpdateBranchResponse> Handle(UpdateBranchRequest request,
            CancellationToken cancellationToken)
        {
            Entities.Branch branch = await _branchRepository.Get(request.Id, cancellationToken);
            if(branch is null)
            {
               throw new KeyNotFoundException($"Branch with ID {request.Id} does not exist in our database");
            }

            branch.Update(request.Name, request.Location);
            await _unitOfWork.Commit(cancellationToken);
            return _mapper.Map<UpdateBranchResponse>(branch);
        }
    }
}
