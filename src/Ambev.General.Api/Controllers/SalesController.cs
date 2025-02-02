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
public class SalesController : ControllerBase
{
    private readonly IMediator _mediator;

    public SalesController(IMediator mediator)
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
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }

    [HttpPut("/sales/cancel/{id}")]
    public async Task<IActionResult> Cancel(int id,
                                            CancellationToken cancellationToken)
    {
        var requestUpdate = new CancelSaleRequest(id);
        var response = await _mediator.Send(requestUpdate, cancellationToken);
        return Ok(response);
    }

    [HttpGet("/sales/{id}")]
    public async Task<ActionResult<GetSaleByIdResponse>> GetById(int id, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetSaleByIdRequest(id), cancellationToken);
        return Ok(response);
    }

    [HttpGet("/sales/{_page}/{_size}/{_order?}")]
    public async Task<ActionResult<GetSalesQueryResponse>> GetByCategories(CancellationToken cancellationToken, int _page = 1, int _size = 10, string _order = "")
    {
        var response = await _mediator.Send(new GetSalesQueryRequest( _page, _size, _order), cancellationToken);
        return Ok(response);
    }
}
