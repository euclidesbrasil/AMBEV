using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ambev.Core.Application.UseCases.Queries.GetProductCategories;
using Ambev.Core.Application.UseCases.Queries.GetProductsByCategories;
using Microsoft.AspNetCore.Authorization;
using Ambev.Application.UseCases.Commands.Product.UpdateProduct;
using Ambev.Application.UseCases.Commands.Product.CreateProduct;
using Ambev.Application.UseCases.Commands.Product.DeleteProduct;

namespace Ambev.General.Api.Controllers;

[Route("[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<CreateProductResponse>> Create(CreateProductRequest request,
                                                         CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }

    // PUT /products/{id}
    [HttpPut]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateProductRequest request,
                                            CancellationToken cancellationToken)
    {
        var requestUpdate = request.Id = id;
        var response = await _mediator.Send(requestUpdate, cancellationToken);
        return Ok(response);
    }

    // DELETE /products/{id}
    [HttpDelete]
    public async Task<IActionResult> Delete(int id,
                                            CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new DeleteProductRequest(id), cancellationToken);
        return Ok(response);
    }

    [HttpGet("/products/categories")]
    public async Task<ActionResult<List<string>>> GetCategories(CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetProductsByCategoriesQuery(), cancellationToken);
        return Ok(response);
    }

    [HttpGet("/products/category/{category}/{_page}/{_size}/{_order?}")]
    public async Task<ActionResult<List<GetProductsByCategoriesResponse>>> GetByCategories(string category, CancellationToken cancellationToken, int _page = 1, int _size = 10, string _order = "")
    {
        var response = await _mediator.Send(new GetProductsByCategoriesRequest(category, _page, _size, _order), cancellationToken);
        return Ok(response);
    }
}
