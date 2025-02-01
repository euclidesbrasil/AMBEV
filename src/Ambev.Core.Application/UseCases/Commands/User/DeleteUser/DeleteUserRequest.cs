using AutoMapper;
using Ambev.Core.Application.UseCases.DTOs;
using Ambev.Core.Domain.Aggregate;
using Ambev.Core.Domain.Interfaces;
using MediatR;

namespace Ambev.Application.UseCases.Commands.User.DeleteUser;

public class DeleteUserRequest : UserDTO, IRequest<DeleteUserResponse>
{
    public DeleteUserRequest(UserDTO user)
    {
        Id = user.Id;
        Name = user.Name;
        Email = user.Email;
    }
}
