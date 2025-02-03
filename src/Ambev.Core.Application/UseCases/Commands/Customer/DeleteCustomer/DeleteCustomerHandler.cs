using AutoMapper;
using Ambev.Core.Domain.Interfaces;
using MediatR;
using Ambev.Core.Domain.Entities;

namespace Ambev.Application.UseCases.Commands.Customer.DeleteCustomer;

public class DeleteCustomerHandler : IRequestHandler<DeleteCustomerRequest, DeleteCustomerResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICustomerRepository _custumerRepository;
    private readonly IMapper _mapper;

    public DeleteCustomerHandler(IUnitOfWork unitOfWork,
        ICustomerRepository custumerRepository,
        IMapper mapper
        )
    {
        _unitOfWork = unitOfWork;
        _custumerRepository = custumerRepository;
        _mapper = mapper;
    }

    public async Task<DeleteCustomerResponse> Handle(DeleteCustomerRequest request, CancellationToken cancellationToken)
    {
        Ambev.Core.Domain.Entities.Customer customer = await _custumerRepository.Get(request.id, cancellationToken);

        if (customer == null)
        {
            throw new KeyNotFoundException("Not found.");
        }

        _custumerRepository.Delete(customer);
        await _unitOfWork.Commit(cancellationToken);
        var userResponse = _mapper.Map<Ambev.Core.Domain.Entities.Customer, DeleteCustomerResponse>(customer);
        return userResponse;
    }
}
