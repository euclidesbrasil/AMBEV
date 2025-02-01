using AutoMapper;
using Ambev.Core.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Ambev.Core.Application.UseCases.Queries.GetSaleById
{
    public class GetSaleById : IRequestHandler<GetSaleByIdRequest, GetSaleByIdResponse>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;

        public GetSaleById(ISaleRepository saleRepository, IMapper mapper)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
        }

        public async Task<GetSaleByIdResponse> Handle(GetSaleByIdRequest request, CancellationToken cancellationToken)
        {
            var sale = await _saleRepository.Get(request.id, cancellationToken);

            if (sale == null)
            {
                throw new KeyNotFoundException("Not found.");
            }


            return _mapper.Map<GetSaleByIdResponse>(sale);
        }
    }
}
