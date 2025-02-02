using AutoMapper;
using Ambev.Core.Domain.Aggregate;
using MediatR;

namespace Ambev.Core.Application.UseCases.Queries.GetBranchsQuery
{

    public sealed record GetBranchsQueryRequest(int page, int size, string order) : IRequest<GetBranchsQueryResponse>;
}