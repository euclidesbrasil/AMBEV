using AutoMapper;
using Ambev.Core.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Collections;

namespace Ambev.Core.Application.UseCases.Queries.GetCartsQuery
{
    public class GetCartsQueryHandler : IRequestHandler<GetCartsQueryRequest, GetCartsQueryResponse>
    {
        private readonly ICartRepository _cartRepository;
        private readonly IMapper _mapper;

        public GetCartsQueryHandler(ICartRepository cartRepository, IMapper mapper)
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
        }

        public async Task<GetCartsQueryResponse> Handle(GetCartsQueryRequest request, CancellationToken cancellationToken)
        {
            var carts = await _cartRepository.GetPaginatedResultAsync(x => true, new Domain.Common.PaginationQuery()
            {
                Order = request.Order,
                Page = request.Page,
                Size = request.Size
            }, cancellationToken);
            List<GetCartsQueryDataResponse> dataFinal = new List<GetCartsQueryDataResponse>();
            foreach(var item in carts.Data)
            {
                dataFinal.Add(_mapper.Map<GetCartsQueryDataResponse>(item));
            }

            return new GetCartsQueryResponse()
            {
                Data = dataFinal,
                CurrentPage = carts.CurrentPage,
                TotalItems = carts.TotalItems,
                TotalPages = carts.TotalPages
            };
        }
    }
}
