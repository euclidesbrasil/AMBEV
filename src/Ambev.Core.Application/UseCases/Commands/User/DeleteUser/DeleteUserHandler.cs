using AutoMapper;
using Ambev.Core.Domain.Interfaces;
using MediatR;
using Ambev.Core.Domain.Entities;

namespace Ambev.Application.UseCases.Commands.User.DeleteUser;

public class DeleteCartHandler : IRequestHandler<DeleteUserRequest, DeleteUserResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public DeleteCartHandler(IUnitOfWork unitOfWork,
        IUserRepository productRepository,
        IMapper mapper
        )
    {
        _unitOfWork = unitOfWork;
        _userRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<DeleteUserResponse> Handle(DeleteUserRequest request, CancellationToken cancellationToken)
    {
        Ambev.Core.Domain.Entities.User user = await _userRepository.Get(request.Id, cancellationToken);

        if (user == null)
        {
            throw new KeyNotFoundException($"User with ID  {request.Id} does not exist in our database");
        }

        _userRepository.Delete(user);
        await _unitOfWork.Commit(cancellationToken);
        var userResponse = _mapper.Map<Ambev.Core.Domain.Entities.User, DeleteUserResponse>(user);
        return userResponse;
    }
}
