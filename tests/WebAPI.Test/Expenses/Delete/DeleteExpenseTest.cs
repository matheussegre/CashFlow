using CashFlow.Domain.Enums;
using CashFlow.Exception;
using FluentAssertions;
using Microsoft.AspNetCore.Routing;
using System.Globalization;
using System.Net;
using System.Text.Json;
using WebAPI.Test.InlineData;

namespace WebAPI.Test.Expenses.Delete;
public class DeleteExpenseTest: CashFlowClassFixture
{
    private const string ROUTE = "api/expenses";

    private readonly string _token;
    private readonly long _expenseId;

    public DeleteExpenseTest(CustomWebApplicationFactory webApplicationFactory) : base(webApplicationFactory)
    {
        _token = webApplicationFactory.User_Team_Member.GetToken();
        _expenseId = webApplicationFactory.Expense_TeamMember.GetId();
    }

    [Fact]
    public async Task Success()
    {
        var result = await DoDelete($"{ROUTE}/{_expenseId}", _token);

        result.StatusCode.Should().Be(HttpStatusCode.NoContent);

        result = await DoGet($"{ROUTE}/{_expenseId}", _token);

        result.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Theory]
    [ClassData(typeof(CultureInlineDataTest))]
    public async Task Error_Expense_Not_Found(string culture)
    {
        var expenseId = 1000;
        var result = await DoDelete($"{ROUTE}/{expenseId}", _token, culture);

        result.StatusCode.Should().Be(HttpStatusCode.NotFound);

        var body = await result.Content.ReadAsStreamAsync();

        var response = await JsonDocument.ParseAsync(body);

        var errors = response.RootElement.GetProperty("errorMessages").EnumerateArray();

        var expectedMessage = ResourceErrorMessages.ResourceManager.GetString("EXPENSE_NOT_FOUND", new CultureInfo(culture));

        errors.Should().HaveCount(1).And.Contain(message => message.GetString()!.Equals(expectedMessage));
    }
}
