using CodingAssignment.Expenses.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CodingAssignment.Expenses.Domain;

public class ExpensesDbContext : DbContext
{
    public ExpensesDbContext(DbContextOptions options) : base(options)
    {
        Database.EnsureCreated();
    }

    public virtual DbSet<Currency> Currencies { get; set; }
    public virtual DbSet<ExpenseType> ExpenseTypes { get; set; }
    public virtual DbSet<Expense> Expenses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Currency>().ToTable("Currency").HasData(
            new Currency { Id = 1, CurrencyCode = "CHF" },
            new Currency { Id = 2, CurrencyCode = "EUR" },
            new Currency { Id = 3, CurrencyCode = "PLN" });

        modelBuilder.Entity<ExpenseType>().ToTable("ExpenseType").HasData(
            new ExpenseType { Id = 1, Name = "Food" },
            new ExpenseType { Id = 2, Name = "Drinks" },
            new ExpenseType { Id = 3, Name = "Other" });

        modelBuilder.Entity<Expense>().ToTable("Expense");
    }
}
