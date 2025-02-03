using AutoMapper;
using Ambev.Core.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Ambev.Core.Application.UseCases.Queries.GetCartQueryId
{
    public class GetCartByIdHandler : IRequestHandler<GetCartByIdRequest, GetCartByIdResponse>
    {
        private readonly ICartRepository _cartRepository;
        private readonly IMapper _mapper;

        public GetCartByIdHandler(ICartRepository cartRepository, IMapper mapper)
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
        }

        public async Task<GetCartByIdResponse> Handle(GetCartByIdRequest request, CancellationToken cancellationToken)
        {
            var carts = await _cartRepository.Filter(x => x.Id == request.id, cancellationToken);
            
            if(carts == null || carts.Count == 0)
            {
                throw new KeyNotFoundException($"Cart with ID  {request.id} does not exist in our database");
            }

            var cardFound = carts.FirstOrDefault();

            return _mapper.Map<GetCartByIdResponse>(cardFound);
        }
    }
}
