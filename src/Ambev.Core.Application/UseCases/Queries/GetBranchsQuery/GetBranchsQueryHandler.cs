using AutoMapper;
using Ambev.Core.Domain.Interfaces;
using MediatR;
using Ambev.Core.Domain.Common;
using Entities = Ambev.Core.Domain.Entities;
using Ambev.Core.Application.UseCases.DTOs;
namespace Ambev.Core.Application.UseCases.Queries.GetBranchsQuery
{
    public class GetBranchsQueryHandler : IRequestHandler<GetBranchsQueryRequest, GetBranchsQueryResponse>
    {
        private readonly IBranchRepository _repository;
        private readonly IMapper _mapper;

        public GetBranchsQueryHandler(IBranchRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GetBranchsQueryResponse> Handle(GetBranchsQueryRequest query, CancellationToken cancellationToken)
        {
            var paginationQuery = new PaginationQuery
            {
                Page = query.page,
                Size = query.size,
                Order = query.order
            };

            PaginatedResult<Entities.Branch> listBranch = await _repository.GetBranchPagination(new PaginationQuery()
            {
                Order = query.order,
                Page = query.page,
                Size = query.size
            }, cancellationToken);

            return new GetBranchsQueryResponse()
            {
                Data = _mapper.Map<List<BranchDTO>>(listBranch.Data),
                CurrentPage = listBranch.CurrentPage,
                TotalItems = listBranch.TotalItems > 0 ? listBranch.TotalItems:0,
                TotalPages = listBranch.TotalPages
            };
        }
    }
}