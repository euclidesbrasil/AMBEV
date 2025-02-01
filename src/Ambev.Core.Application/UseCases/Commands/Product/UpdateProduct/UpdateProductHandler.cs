using AutoMapper;
using Ambev.Core.Domain.Interfaces;
using MediatR;
using Ambev.Core.Domain.Entities;
using Ambev.Core.Domain.ValueObjects;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Ambev.Application.UseCases.Commands.Product.UpdateProduct;

public class UpdateProductHandler :
       IRequestHandler<UpdateProductRequest, UpdateProductResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public UpdateProductHandler(IUnitOfWork unitOfWork,
        IProductRepository productRepository,
        IMapper mapper
        )
    {
        _unitOfWork = unitOfWork;
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<UpdateProductResponse> Handle(UpdateProductRequest request,
        CancellationToken cancellationToken)
    {

        Ambev.Core.Domain.Entities.Product product = await _productRepository.Get(request.Id, cancellationToken);
        product.Update(request.Title, request.Price, request.Description, request.Category, request.Image, new Rating(request.Rating.Rate, request.Rating.Count));
        _productRepository.Update(product);

        await _unitOfWork.Commit(cancellationToken);
        return _mapper.Map<UpdateProductResponse>(product);
    }
}
