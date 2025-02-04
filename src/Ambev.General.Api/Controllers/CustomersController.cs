using Ambev.Application.UseCases.Commands.Customer.CreateCustomer;
using Ambev.Application.UseCases.Commands.Customer.DeleteCustomer;
using Ambev.Application.UseCases.Commands.Customer.UpdateCustomer;
using Ambev.Core.Application.UseCases.Queries.GetCustomersQuery;
using Ambev.Core.Application.UseCases.Queries.GetCustomerById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Ambev.General.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class CustomersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<CreateCustomerResponse>> Create(CreateCustomerRequest request,
                                                             CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCustomerRequest request,
                                                CancellationToken cancellationToken)
        {
            request.Id = id;
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id,
                                                CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new DeleteCustomerRequest(id), cancellationToken);
            return Ok(response);
        }

        [HttpGet("/customers/{id}")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetCustomerByIdRequest(id), cancellationToken);
            return Ok(response);
        }

        [HttpGet("/customers")]
        public async Task<ActionResult<List<GetCustomersQueryResponse>>> GetCustomersQuery(CancellationToken cancellationToken, int _page = 1, int _size = 10, [FromQuery] Dictionary<string, string> filters = null, string _order = "id asc")
        {
            filters = filters ?? new Dictionary<string, string>();
            filters = HttpContext.Request.Query
            .Where(q => q.Key != "_page" && q.Key != "_size" && q.Key != "_order")
            .ToDictionary(q => q.Key, q => q.Value.ToString());

            var response = await _mediator.Send(new GetCustomersQueryRequest(_page, _size, _order, filters), cancellationToken);
            return Ok(response);
        }
    }
}
