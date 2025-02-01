using AutoMapper;
using Ambev.Core.Domain.Interfaces;
using MediatR;
using Ambev.Core.Domain.Entities;

namespace Ambev.Application.UseCases.Commands.Cart.DeleteCart;

public class DeleteCartHandler : IRequestHandler<DeleteCartRequest, DeleteCartResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICartRepository _cartRepository;
    private readonly IMapper _mapper;

    public DeleteCartHandler(IUnitOfWork unitOfWork,
        ICartRepository cartRepository,
        IMapper mapper
        )
    {
        _unitOfWork = unitOfWork;
        _cartRepository = cartRepository;
        _mapper = mapper;
    }

    public async Task<DeleteCartResponse> Handle(DeleteCartRequest request, CancellationToken cancellationToken)
    {
        var carts = await _cartRepository.Filter(x => x.Id == request.id, cancellationToken);

        if (carts == null || carts.Count == 0)
        {
            throw new KeyNotFoundException("Not found.");
        }

        var cart = carts.FirstOrDefault();
        await _cartRepository.Delete(cart);
        await _unitOfWork.Commit(cancellationToken);
        return new DeleteCartResponse("Cart removed.");
    }
}
