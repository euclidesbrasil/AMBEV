using AutoMapper;
using Entities = Ambev.Core.Domain.Entities;
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
    public class CreateCartHandler : IRequestHandler<CreateCartRequest, CreateCartResponse>
    {
        private readonly ICartRepository _cartRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public CreateCartHandler(ICartRepository cartRepository, IProductRepository productRepository, IUserRepository userRepository, IMapper mapper)
        {
            _cartRepository = cartRepository;
            _productRepository = productRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<CreateCartResponse> Handle(CreateCartRequest request, CancellationToken cancellationToken)
        {
            var cart = new Ambev.Core.Domain.Entities.Cart(request.UserId, request.Date, request.Products.Select(p =>
            new CartItem(p.ProductId, p.Quantity)).ToList());
            var user = await _userRepository.Get(request.UserId, cancellationToken);
            if(user is null)
            {
                throw new KeyNotFoundException($"User with ID {request.UserId} does not exist in our database");
            }
            var idsProducts = cart.Products.Select(x => x.ProductId).Distinct().ToList();
            var allProducts = await _productRepository.GetProductByListIdsAsync(idsProducts, cancellationToken);
            allProducts = allProducts ?? new List<Core.Domain.Entities.Product>();
            var productsNotSavedInDataBase = idsProducts.Except(allProducts.Select(p => p.Id)).ToList();
            if (productsNotSavedInDataBase.Count > 0)
            {
                throw new ArgumentNullException($"Products not found ({string.Join(",", productsNotSavedInDataBase.Distinct())}).");
            }
            await _cartRepository.Create(cart);
            return _mapper.Map<CreateCartResponse>(cart);
        }
    }
}
