using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ambev.Application.UseCases.Commands.User.UpdateUser;
using Ambev.Application.UseCases.Commands.User.CreateUser;
using Ambev.Core.Application.UseCases.Queries.GetUsersById;
using Ambev.Core.Application.UseCases.Queries.GetUsersQuery;

namespace Ambev.General.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<CreateUserResponse>> Create(CreateUserRequest request,
                                                             CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        // PUT /users/{id}
        [HttpPut]
        public async Task<ActionResult<UpdateUserResponse>> Update(int id, [FromBody] UpdateUserRequest request,
                                                CancellationToken cancellationToken)
        {
            request.Update(id);
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }

      

        [HttpGet("/users/{id}")]
        public async Task<ActionResult<List<string>>> GetById(int id,CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetUsersByIdRequest(id), cancellationToken);
            return Ok(response);
        }

        [HttpGet("/users")]
        public async Task<ActionResult<List<GetUsersQueryResponse>>> GetByCategories(CancellationToken cancellationToken, int _page = 1, int _size = 10, string _order = "id asc")
        {
            var response = await _mediator.Send(new GetUsersQueryRequest(_page, _size, _order), cancellationToken);
            return Ok(response);
        }
    }
}
