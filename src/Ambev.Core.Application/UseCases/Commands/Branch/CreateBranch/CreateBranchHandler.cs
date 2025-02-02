using Ambev.Core.Domain.Interfaces;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities = Ambev.Core.Domain.Entities;
namespace Ambev.Core.Application.UseCases.Commands.Sale.CreateBranch
{
    public class CreateBranchHandler :
       IRequestHandler<CreateBranchRequest, CreateBranchResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBranchRepository _branchRepository;
        private readonly IMapper _mapper;

        public CreateBranchHandler(IUnitOfWork unitOfWork,
            IBranchRepository branchRepository,
            IMapper mapper
            )
        {
            _unitOfWork = unitOfWork;
            _branchRepository = branchRepository;
            _mapper = mapper;
        }

        public async Task<CreateBranchResponse> Handle(CreateBranchRequest request,
            CancellationToken cancellationToken)
        {
            var branch = _mapper.Map<Entities.Branch>(request);
            _branchRepository.Create(branch);

            await _unitOfWork.Commit(cancellationToken);
            return _mapper.Map<CreateBranchResponse>(branch);
        }
    }
}
