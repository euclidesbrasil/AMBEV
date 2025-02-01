using AutoMapper;
using Ambev.Core.Domain.Entities;
using Ambev.Core.Domain.Interfaces;
using Ambev.Core.Domain.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.Application.UseCases.Commands.Cart.CreateCart
{
    public class AddCartCommandHandler : IRequestHandler<CreateCartRequest, CreateCartResponse>
    {
        private readonly ICartRepository _cartRepository;
        private readonly IMapper _mapper;

        public AddCartCommandHandler(ICartRepository cartRepository, IMapper mapper)
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
        }

        public async Task<CreateCartResponse> Handle(CreateCartRequest request, CancellationToken cancellationToken)
        {
            var cart = new Ambev.Core.Domain.Entities.Cart(request.UserId, request.Date, request.Products.Select(p =>
            new CartItem(p.ProductId, p.Quantity)).ToList());

            await _cartRepository.Create(cart);
            var cartSaved = _mapper.Map<Ambev.Core.Domain.Entities.Cart, CreateCartResponse>(cart);
            return cartSaved;
        }
    }
}
