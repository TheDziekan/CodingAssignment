namespace CodingAssignment.Expenses.Domain.Entities;

public class Expense
{
    public long Id { get; set; }
    public DateOnly TransactionDate { get; set; }
    public string Recipient { get; set; }
    public decimal Amount { get; set; }

    public int CurrencyId { get; set; }
    public Currency Currency { get; set; }

    public int ExpenseTypeId { get; set; }
    public ExpenseType ExpenseType { get; set; }
}
