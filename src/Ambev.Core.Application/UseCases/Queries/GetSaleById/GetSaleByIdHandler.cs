using AutoMapper;
using Ambev.Core.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Ambev.Core.Application.UseCases.DTOs;
using Ambev.Core.Domain.Entities;

namespace Ambev.Core.Application.UseCases.Queries.GetSaleById
{
    public class GetSaleById : IRequestHandler<GetSaleByIdRequest, GetSaleByIdResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISaleRepository _saleRepository;
        private readonly IUserRepository _userRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetSaleById(IUnitOfWork unitOfWork,
            ISaleRepository saleRepository,
            IUserRepository userRepository,
            IProductRepository productRepository,
            IMapper mapper
            )
        {
            _unitOfWork = unitOfWork;
            _saleRepository = saleRepository;
            _userRepository = userRepository;
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public async Task<GetSaleByIdResponse> Handle(GetSaleByIdRequest request, CancellationToken cancellationToken)
        {

            var sale = await _saleRepository.GetSaleWithItemsAsync(request.id, cancellationToken);

            if (sale is null)
                throw new KeyNotFoundException("Not found.");

            return _mapper.Map<GetSaleByIdResponse>(sale);
        }
    }
}
