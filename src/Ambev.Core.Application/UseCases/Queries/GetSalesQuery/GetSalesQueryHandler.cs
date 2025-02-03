using AutoMapper;
using Ambev.Core.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Ambev.Core.Application.UseCases.Queries.GetSaleById;
using Ambev.Core.Application.UseCases.Queries.GetProductsByCategories;
using Ambev.Core.Application.UseCases.DTOs;

namespace Ambev.Core.Application.UseCases.Queries.GetSalesQuery
{
    public class GetSalesQueryHandler : IRequestHandler<GetSalesQueryRequest, GetSalesQueryResponse>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;

        public GetSalesQueryHandler(ISaleRepository saleRepository, IMapper mapper)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
        }

        public async Task<GetSalesQueryResponse> Handle(GetSalesQueryRequest request, CancellationToken cancellationToken)
        {
            var sales = await _saleRepository.GetSalesPagination(new Domain.Common.PaginationQuery()
            {
                Order = request.order,
                Page = request.page,
                Size = request.size,
                Filter = request.filters
            }, cancellationToken);

            List<SaleWithDetaislsDTO> itensReturn = new List<SaleWithDetaislsDTO>();
            foreach(var item in sales.Data)
            {
                itensReturn.Add(_mapper.Map<SaleWithDetaislsDTO>(item));
            }

            return new GetSalesQueryResponse()
            {
                Data = itensReturn,
                CurrentPage = sales.CurrentPage,
                TotalItems = sales.TotalItems,
                TotalPages = sales.TotalPages
            };
        }
    }
}
