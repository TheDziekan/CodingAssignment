using CodingAssignment.Expenses.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CodingAssignment.Expenses.Api.ExpenseType;

public static class ExpenseTypeSelectList
{
    public sealed class Request : IRequest<IList<Response>>
    {
    }

    public sealed class Response
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public sealed class Handler : IRequestHandler<Request, IList<Response>>
    {
        private readonly ExpensesDbContext _context;

        public Handler(ExpensesDbContext context)
        {
            _context = context;
        }

        public async Task<IList<Response>> Handle(Request request, CancellationToken cancellationToken)
        {
            return await _context.ExpenseTypes
                .AsNoTracking()
                .Select(x => new Response
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .ToListAsync(cancellationToken);
        }
    }
}
