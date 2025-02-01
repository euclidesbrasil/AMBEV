using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ambev.Core.Application.UseCases.Queries.GetProductCategories;
using Ambev.Core.Application.UseCases.Queries.GetProductsByCategories;
using Microsoft.AspNetCore.Authorization;
using Ambev.Core.Application.UseCases.Commands.Sale.CancelSale;
using Ambev.Core.Application.UseCases.Commands.Sale.UpdateSale;
using Ambev.Core.Application.UseCases.Commands.Sale.CreateSale;
using Ambev.Core.Application.UseCases.Queries.GetUsersById;
using Ambev.Core.Application.UseCases.Queries.GetSalesQuery;
using Ambev.Core.Application.UseCases.Queries.GetSaleById;

namespace Ambev.General.Api.Controllers;

[Route("[controller]")]
[ApiController]
public class SaleController : ControllerBase
{
    private readonly IMediator _mediator;

    public SaleController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<CreateSaleResponse>> Create(CreateSaleRequest request,
                                                         CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }

    [HttpPut]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateSaleRequest request,
                                            CancellationToken cancellationToken)
    {
        var requestUpdate = request.Id = id;
        var response = await _mediator.Send(requestUpdate, cancellationToken);
        return Ok(response);
    }

    [HttpPut("/sale/cancel/{id}")]
    public async Task<IActionResult> Cancel(int id,
                                            CancellationToken cancellationToken)
    {
        var requestUpdate = new CancelSaleRequest(id);
        var response = await _mediator.Send(requestUpdate, cancellationToken);
        return Ok(response);
    }

    [HttpGet("/sale/{id}")]
    public async Task<ActionResult<List<string>>> GetById(int id, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetUsersByIdRequest(id), cancellationToken);
        return Ok(response);
    }

    [HttpGet("/sale/{_page}/{_size}/{_order?}")]
    public async Task<ActionResult<GetSalesQueryResponse>> GetByCategories(CancellationToken cancellationToken, int _page = 1, int _size = 10, string _order = "")
    {
        var response = await _mediator.Send(new GetSalesQueryRequest( _page, _size, _order), cancellationToken);
        return Ok(response);
    }
}
