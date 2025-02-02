using AutoMapper;
using Ambev.Core.Domain.Interfaces;
using MediatR;
using Entities = Ambev.Core.Domain.Entities;
using Ambev.Application.UseCases.Commands.Cart.DeleteCart;

namespace Ambev.Core.Application.UseCases.Commands.Branch.DeleteBranch;

public class DeleteBranchHandler : IRequestHandler<DeleteBranchRequest, DeleteBranchResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBranchRepository _branchRepository;
    private readonly IMapper _mapper;

    public DeleteBranchHandler(IUnitOfWork unitOfWork,
        IBranchRepository branchRepository,
        IMapper mapper
        )
    {
        _unitOfWork = unitOfWork;
        _branchRepository = branchRepository;
        _mapper = mapper;
    }

    public async Task<DeleteBranchResponse> Handle(DeleteBranchRequest request, CancellationToken cancellationToken)
    {
        Entities.Branch branch = await _branchRepository.Get(request.id, cancellationToken);

        if (branch is null)
        {
            throw new KeyNotFoundException("Not found.");
        }

        _branchRepository.Delete(branch);
        await _unitOfWork.Commit(cancellationToken);
        return new DeleteBranchResponse("Branch removed.");
    }
}
