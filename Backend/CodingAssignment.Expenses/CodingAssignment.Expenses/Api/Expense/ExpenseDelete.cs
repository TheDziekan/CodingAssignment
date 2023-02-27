using CodingAssignment.Expenses.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CodingAssignment.Expenses.Api.Expense;

public static class ExpenseDelete
{
    public sealed class Request : IRequest
    {
        public long Id { get; set; }
    }

    public sealed class Handler : IRequestHandler<Request>
    {
        private readonly ExpensesDbContext _context;

        public Handler(ExpensesDbContext context)
        {
            _context = context;
        }

        public async Task Handle(Request request, CancellationToken cancellationToken)
        {
            _ = await _context.Expenses.Where(x => x.Id == request.Id).ExecuteDeleteAsync(cancellationToken);
        }
    }
}
