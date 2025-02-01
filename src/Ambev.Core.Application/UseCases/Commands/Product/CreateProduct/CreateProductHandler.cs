using AutoMapper;
using Ambev.Core.Domain.Aggregate;
using Ambev.Core.Domain.Interfaces;
using MediatR;
using Ambev.Core.Domain.Entities;

namespace Ambev.Application.UseCases.Commands.Product.CreateProduct;

public class CreateProductHandler :
       IRequestHandler<CreateProductRequest, CreateProductResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public CreateProductHandler(IUnitOfWork unitOfWork,
        IProductRepository productRepository,
        IMapper mapper
        )
    {
        _unitOfWork = unitOfWork;
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<CreateProductResponse> Handle(CreateProductRequest request,
        CancellationToken cancellationToken)
    {
        var product = _mapper.Map<Ambev.Core.Domain.Entities.Product>(request);

        _productRepository.Create(product);

        await _unitOfWork.Commit(cancellationToken);
        return _mapper.Map<CreateProductResponse>(product);
    }
}
