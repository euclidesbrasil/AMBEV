using AutoMapper;
using Ambev.Core.Domain.Aggregate;
using Ambev.Core.Domain.Entities;
using Ambev.Core.Domain.Interfaces;
using MediatR;

namespace Ambev.Application.UseCases.Commands.Customer.UpdateCustomer;

public class UpdateCustomerHandler :
       IRequestHandler<UpdateCustomerRequest, UpdateCustomerResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public UpdateCustomerHandler(IUnitOfWork unitOfWork,
        ICustomerRepository customerRepository,
        IMapper mapper
        )
    {
        _unitOfWork = unitOfWork;
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    public async Task<UpdateCustomerResponse> Handle(UpdateCustomerRequest request,
        CancellationToken cancellationToken)
    {

        var custumer = await _customerRepository.Get(request.Id, cancellationToken);
        custumer.Update(request.FirstName, request.LastName, request.Identification);
        await _unitOfWork.Commit(cancellationToken);
        return _mapper.Map<UpdateCustomerResponse>(custumer);
    }
}
