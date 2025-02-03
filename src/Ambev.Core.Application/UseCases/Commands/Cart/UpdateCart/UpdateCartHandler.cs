using AutoMapper;
using Ambev.Core.Domain.Interfaces;
using MediatR;
using Ambev.Core.Domain.Entities;
using Ambev.Core.Domain.ValueObjects;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Ambev.Core.Application.UseCases.DTOs;

namespace Ambev.Application.UseCases.Commands.Cart.UpdateCart;

public class UpdateCartHandler :
       IRequestHandler<UpdateCartRequest, UpdateCartResponse>
{
    private readonly ICartRepository _cartRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    public UpdateCartHandler(ICartRepository cartRepository, IProductRepository productRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _cartRepository = cartRepository;
        _productRepository = productRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<UpdateCartResponse> Handle(UpdateCartRequest request,
        CancellationToken cancellationToken)
    {

        var carts = await _cartRepository.Filter(x => x.Id == request.GetIdContext(), cancellationToken);
        if (carts == null || carts.Count == 0)
        {
            throw new KeyNotFoundException($"Cart with ID  {request.Id} does not exist in our database");
        }

        var cartUpdate = carts.FirstOrDefault();

        List<CartItem> itens = new List<CartItem>();
        foreach (var item in request.Products)
        {
            itens.Add(_mapper.Map<CartItem>(item));
        }

        var idsProducts = itens.Select(x => x.ProductId).Distinct().ToList();
        var allProducts = await _productRepository.Filter(x => idsProducts.Contains(x.Id), cancellationToken);
        var productsNotSavedInDataBase = idsProducts.Except(allProducts.Select(p => p.Id)).ToList();
        if (productsNotSavedInDataBase.Count > 0)
        {
            throw new KeyNotFoundException($"Products with ID {string.Join(",", productsNotSavedInDataBase.Distinct())} does not exist in our database");
        }

        cartUpdate.RemoveAllProducts();
        cartUpdate.Update(request.UserId, request.Date, itens);

        await _cartRepository.Update(cartUpdate);
        await _unitOfWork.Commit(cancellationToken);
        return _mapper.Map<UpdateCartResponse>(cartUpdate);
    }
}
