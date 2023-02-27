using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CodingAssignment.Expenses.Api.Expense;

[ApiController]
[Route("[controller]")]
public class ExpenseController : ControllerBase
{
    private readonly IMediator _mediator;

    public ExpenseController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> List(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new ExpenseList.Request(), cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Read([FromRoute] long id, CancellationToken cancellationToken)
    {
        var request = new ExpenseRead.Request()
        {
            Id = id
        };
        var result = await _mediator.Send(request, cancellationToken);

        if (result is null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromServices] IValidator<ExpenseCreate.Request> validator,
        [FromBody] ExpenseCreate.Request request,
        CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.ToDictionary());
        }

        var result = await _mediator.Send(request, cancellationToken);
        return Ok(result.Id); // Could also use 201 Created
    }

    [HttpPut] //could also move id to route
    public async Task<IActionResult> Update(
        [FromServices] IValidator<ExpenseUpdate.Request> validator,
        [FromBody] ExpenseUpdate.Request request,
        CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.ToDictionary());
        }

        var result = await _mediator.Send(request, cancellationToken);

        if (!result.IsSuccess)
        {
            return NotFound();
        }

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] long id, CancellationToken cancellationToken)
    {
        var request = new ExpenseDelete.Request()
        {
            Id = id
        };

        await _mediator.Send(request, cancellationToken);

        // NotFoud result omitted by design

        return Ok();
    }
}
