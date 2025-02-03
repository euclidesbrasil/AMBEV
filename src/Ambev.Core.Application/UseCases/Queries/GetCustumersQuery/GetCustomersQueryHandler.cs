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
using System.Collections;

namespace Ambev.Core.Application.UseCases.Queries.GetCustomersQuery
{ 
    public class GetCustomersQueryHandler : IRequestHandler<GetCustomersQueryRequest, GetCustomersQueryResponse>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public GetCustomersQueryHandler(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<GetCustomersQueryResponse> Handle(GetCustomersQueryRequest request, CancellationToken cancellationToken)
        {
            var custumers = await _customerRepository.GetCustumerPagination(new Domain.Common.PaginationQuery()
            {
                Order = request.order,
                Page = request.page,
                Size = request.size,
                Filter = request.filters
            }, cancellationToken);

            List<CustomerDTO> itensReturn = new List<CustomerDTO>();
            foreach(var item in custumers.Data)
            {
                itensReturn.Add(_mapper.Map<CustomerDTO>(item));
            }

            return new GetCustomersQueryResponse()
            {
                Data = itensReturn,
                CurrentPage = custumers.CurrentPage,
                TotalItems = custumers.TotalItems,
                TotalPages = custumers.TotalPages
            };
        }
    }
}
