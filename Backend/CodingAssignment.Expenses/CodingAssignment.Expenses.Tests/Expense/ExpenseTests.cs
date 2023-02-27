using CodingAssignment.Expenses.Api.Expense;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace CodingAssignment.Expenses.Tests.Expense;

public class ExpenseTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public ExpenseTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task Add_returns_OkAsync()
    {
        //Arrange
        var client = _factory.CreateClient();

        var request = new ExpenseCreate.Request
        {
            TransactionDate = new DateOnly(2023, 2, 24),
            Recipient = "test",
            Amount = 12.34M,
            CurrencyId = 1,
            ExpenseTypeId = 2
        };

        var json = JsonSerializer.Serialize(request);

        var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

        //Act
        var actResponse = await client.PostAsync("/Expense", stringContent);


        //Assert
        actResponse.EnsureSuccessStatusCode();

        var id = await actResponse.Content.ReadAsStringAsync();
        
        var assertResponse = await client.GetAsync($"/Expense/{id}");
        assertResponse.EnsureSuccessStatusCode();

        var response = await assertResponse.Content.ReadFromJsonAsync<ExpenseRead.Response>();

        Assert.NotNull(response);
        Assert.Equal(request.TransactionDate, response.TransactionDate);
        Assert.Equal(request.Recipient, response.Recipient);
        Assert.Equal(request.Amount, response.Amount);
        Assert.Equal("CHF", response.Currency);
        Assert.Equal("Drinks", response.ExpenseType);

        //Annul
        var annulResponse = await client.DeleteAsync($"/Expense/{id}");
        annulResponse.EnsureSuccessStatusCode();
    }
}
