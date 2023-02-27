using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CodingAssignment.Expenses.Api.ExpenseType;

[ApiController]
[Route("[controller]")]
public class ExpenseTypeController : ControllerBase
{
    private readonly IMediator _mediator;

    public ExpenseTypeController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> SelectList(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new ExpenseTypeSelectList.Request(), cancellationToken);
        return Ok(result);
    }
}