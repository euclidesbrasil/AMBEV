using AutoMapper;
using Ambev.Core.Domain.Aggregate;
using Ambev.Core.Domain.Entities;
using Ambev.Core.Domain.Interfaces;
using Ambev.Core.Domain.ValueObjects;
using MediatR;

namespace Ambev.Application.UseCases.Commands.User.CreateUser;

public class CreateUserHandler :
       IRequestHandler<CreateUserRequest, CreateUserResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IJwtTokenService _tokenService;

    public CreateUserHandler(IUnitOfWork unitOfWork,
        IUserRepository userRepository,
        IMapper mapper,
        IJwtTokenService tokenService
        )
    {
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
        _mapper = mapper;
        _tokenService = tokenService;
    }

    public async Task<CreateUserResponse> Handle(CreateUserRequest request,
        CancellationToken cancellationToken)
    {

        var user = _mapper.Map<Ambev.Core.Domain.Entities.User>(request);
        user.ChangePassword(request.Password, _tokenService);
        user.UpdateName(request.Name.Firstname, request.Name.Lastname);
        var address = _mapper.Map<Address>(request.Address);

        _userRepository.Create(user);

        await _unitOfWork.Commit(cancellationToken);
        return _mapper.Map<CreateUserResponse>(user);
    }
}
