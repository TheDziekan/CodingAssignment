using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CodingAssignment.Expenses.Api.Currency;

[ApiController]
[Route("[controller]")]
public class CurrencyController : ControllerBase
{
    private readonly IMediator _mediator;

    public CurrencyController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> SelectList(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new CurrencySelectList.Request(), cancellationToken);
        return Ok(result);
    }
}