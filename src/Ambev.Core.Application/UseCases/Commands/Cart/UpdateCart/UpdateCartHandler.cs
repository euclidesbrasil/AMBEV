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
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICartRepository _cartRepository;
    private readonly IMapper _mapper;

    public UpdateCartHandler(IUnitOfWork unitOfWork,
        ICartRepository cartRepository,
        IMapper mapper
        )
    {
        _unitOfWork = unitOfWork;
        _cartRepository = cartRepository;
        _mapper = mapper;
    }

    public async Task<UpdateCartResponse> Handle(UpdateCartRequest request,
        CancellationToken cancellationToken)
    {

        var carts = await _cartRepository.Filter(x => x.Id == request.GetIdContext(), cancellationToken);
        if (carts == null || carts.Count == 0)
        {
            throw new KeyNotFoundException("Not found.");
        }
        var cartUpdate = carts.FirstOrDefault();

        List<CartItem> itens = new List<CartItem>();
        foreach (var item in request.Products)
        {
            itens.Add(_mapper.Map<CartItem>(item));
        }
        cartUpdate.RemoveAllProducts();
        cartUpdate.Update(request.UserId, request.Date, itens);

        _cartRepository.Update(cartUpdate);

        await _unitOfWork.Commit(cancellationToken);
        return _mapper.Map<UpdateCartResponse>(cartUpdate);
    }
}
