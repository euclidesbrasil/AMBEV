using MediatR;

namespace Ambev.Core.Application.UseCases.Queries.GetCustomersQuery
{
    public sealed record GetCustomersQueryRequest(int page, int size, string order) : IRequest<GetCustomersQueryResponse>;
}
