using CodingAssignment.Expenses.Domain;
using FluentValidation;
using MediatR;

namespace CodingAssignment.Expenses.Api.Expense;

public static class ExpenseUpdate
{
    public sealed class Request : IRequest<Response>
    {
        public long Id { get; set; }
        public DateOnly TransactionDate { get; set; }
        public string Recipient { get; set; }
        public decimal Amount { get; set; }
        public int CurrencyId { get; set; }
        public int ExpenseTypeId { get; set; }
    }

    public sealed class RequestValidator : AbstractValidator<Request>
    {
        public RequestValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .GreaterThan(0);

            RuleFor(x => x.TransactionDate)
                .NotEmpty();

            RuleFor(x => x.Recipient)
                .NotEmpty();

            RuleFor(x => x.Amount)
                .NotEmpty()
                .GreaterThan(decimal.Zero);

            RuleFor(x => x.CurrencyId)
                .NotEmpty()
                .GreaterThan(0);

            RuleFor(x => x.ExpenseTypeId)
                .NotEmpty()
                .GreaterThan(0);
        }

        //For disscusion: Validating foreign key constraints to avoid exception from EF
    }

    public sealed class Response
    {
        public bool IsSuccess { get; set; }
    }

    public sealed class Handler : IRequestHandler<Request, Response>
    {
        private readonly ExpensesDbContext _context;

        public Handler(ExpensesDbContext context)
        {
            _context = context;
        }

        public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
        {
            var entity = await _context.Expenses.FindAsync(request.Id, cancellationToken);

            if (entity is null)
            {
                return new Response { IsSuccess = false };
            }

            entity.TransactionDate = request.TransactionDate;
            entity.Recipient = request.Recipient;
            entity.Amount = request.Amount;
            entity.CurrencyId = request.CurrencyId;
            entity.ExpenseTypeId = request.ExpenseTypeId;

            _ = await _context.SaveChangesAsync();

            return new Response { IsSuccess = true };
        }
    }
}
