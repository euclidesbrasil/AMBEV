using Ambev.Core.Application.UseCases.DTOs;
using MediatR;

namespace Ambev.Application.UseCases.Commands.User.UpdateUser
{
    public class UpdateUserRequest : UserBaseDTO, IRequest<UpdateUserResponse>
    {
        public int Id { get; set; }
    }
}
