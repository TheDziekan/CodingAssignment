using CodingAssignment.Expenses.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CodingAssignment.Expenses.Api.Expense;

public static class ExpenseRead
{
    public sealed class Request : IRequest<Response?>
    {
        public long Id { get; set; }
    }

    public sealed class Response
    {
        public long Id { get; set; }
        public DateOnly TransactionDate { get; set; }
        public string Recipient { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string ExpenseType { get; set; }
    }

    public sealed class Handler : IRequestHandler<Request, Response?>
    {
        private readonly ExpensesDbContext _context;

        public Handler(ExpensesDbContext context)
        {
            _context = context;
        }

        public async Task<Response?> Handle(Request request, CancellationToken cancellationToken)
        {
            return await _context.Expenses
                .AsNoTracking()
                .Select(x => new Response
                {
                    Id = x.Id,
                    TransactionDate = x.TransactionDate,
                    Recipient = x.Recipient,
                    Amount = x.Amount,
                    Currency = x.Currency.CurrencyCode,
                    ExpenseType = x.ExpenseType.Name
                })
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        }
    }
}
