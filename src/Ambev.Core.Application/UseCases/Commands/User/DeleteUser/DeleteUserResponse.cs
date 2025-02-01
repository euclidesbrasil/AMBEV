using Ambev.Core.Application.UseCases.DTOs;

namespace Ambev.Application.UseCases.Commands.User.DeleteUser;

public class DeleteUserResponse : UserDTO
{
    public DeleteUserResponse(UserDTO user)
    {
        Id = user.Id;
        Name = user.Name;
        Email = user.Email;
    }
}