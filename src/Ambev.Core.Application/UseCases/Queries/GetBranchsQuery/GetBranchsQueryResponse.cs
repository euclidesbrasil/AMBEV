using Ambev.Core.Application.UseCases.DTOs;
using Ambev.Core.Domain.Common;
using Ambev.Core.Domain.Interfaces;

namespace Ambev.Core.Application.UseCases.Queries.GetBranchsQuery
{
    public sealed record GetBranchsQueryResponse
    {
        public List<BranchDTO> Data { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
    }

}