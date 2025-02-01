using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using Ambev.Application.UseCases.Commands.Product.DeleteProduct;
using Ambev.Core.Application.UseCases.Queries.GetProductCategories;
using Ambev.Core.Application.UseCases.Queries.GetProductsByCategories;
using Microsoft.AspNetCore.Authorization;
using Ambev.Core.Application.UseCases.DTOs;
using Ambev.Core.Application.UseCases.Queries.GetUsersById;
using Ambev.Application.UseCases.Commands.User.UpdateUser;
using Ambev.Application.UseCases.Commands.User.CreateUser;

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

        // DELETE /users/{id}
        [HttpDelete]
        public async Task<IActionResult> Delete(int id,
                                                CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new DeleteProductRequest(id), cancellationToken);
            return Ok(response);
        }

        [HttpGet("/users/{id}")]
        public async Task<ActionResult<List<string>>> GetById(int id,CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetUsersByIdRequest(id), cancellationToken);
            return Ok(response);
        }

        [HttpGet("/users/{_page}/{_size}/{_order?}")]
        public async Task<ActionResult<List<GetProductsByCategoriesResponse>>> GetByCategories(string category, CancellationToken cancellationToken, int _page = 1, int _size = 10, string _order = "")
        {
            var response = await _mediator.Send(new GetProductsByCategoriesRequest(category, _page, _size, _order), cancellationToken);
            return Ok(response);
        }
    }
}
