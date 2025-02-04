using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ambev.Core.Application.UseCases.Queries.GetCartsQuery;
using Ambev.Core.Application.UseCases.Queries.GetCartQueryId;
using Ambev.Application.UseCases.Commands.Cart.CreateCart;
using Ambev.Application.UseCases.Commands.Cart.UpdateCart;
using Ambev.Application.UseCases.Commands.Cart.DeleteCart;
using Microsoft.AspNetCore.Authorization;

namespace Ambev.General.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class CartsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CartsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // WebApi/Controllers/CartsController.cs
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int _page = 1, [FromQuery] int _size = 10, [FromQuery] string _order = "")
        {
            var query = new GetCartsQueryRequest("", _page, _size, _order);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Get([FromBody] CreateCartRequest request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpGet("/carts/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var query = new GetCartByIdRequest(id);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPut("/carts/{id}")]
        public async Task<IActionResult> Update(int id,[FromBody] UpdateCartRequest request, CancellationToken cancellationToken)
        {
            request.SetIdContext(id);
            var result = await _mediator.Send(request, cancellationToken);
            return Ok(result);
        }

        [HttpDelete("/carts/{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            DeleteCartRequest request = new DeleteCartRequest(id);
            var result = await _mediator.Send(request, cancellationToken);
            return Ok(result);
        }
    }
}
