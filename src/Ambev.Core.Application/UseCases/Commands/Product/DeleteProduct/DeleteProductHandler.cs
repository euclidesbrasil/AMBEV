using AutoMapper;
using Ambev.Core.Domain.Interfaces;
using MediatR;
using Ambev.Core.Domain.Entities;

namespace Ambev.Application.UseCases.Commands.Product.DeleteProduct;

public class DeleteProductHandler : IRequestHandler<DeleteProductRequest, DeleteProductResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public DeleteProductHandler(IUnitOfWork unitOfWork,
        IProductRepository productRepository,
        IMapper mapper
        )
    {
        _unitOfWork = unitOfWork;
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<DeleteProductResponse> Handle(DeleteProductRequest request, CancellationToken cancellationToken)
    {
        Ambev.Core.Domain.Entities.Product product = await _productRepository.Get(request.id, cancellationToken);

        if (product == null)
        {
            throw new KeyNotFoundException("Not found.");
        }

        _productRepository.Delete(product);
        await _unitOfWork.Commit(cancellationToken);

        return new DeleteProductResponse("Product deleted with success.");
    }
}
