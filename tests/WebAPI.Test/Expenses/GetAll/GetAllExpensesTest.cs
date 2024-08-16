using FluentAssertions;
using System.Net;
using System.Text.Json;

namespace WebAPI.Test.Expenses.GetAll;
public class GetAllExpensesTest : CashFlowClassFixture
{
    private const string ROUTE = "api/expenses";
    private readonly string _token;

    public GetAllExpensesTest(CustomWebApplicationFactory customWebApplication) : base(customWebApplication)
    {
        _token = customWebApplication.User_Team_Member.GetToken();
    }

    [Fact]
    public async Task Success()
    {
        var result = await DoGet(ROUTE, _token);

        result.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await result.Content.ReadAsStreamAsync();

        var response = await JsonDocument.ParseAsync(body);

        response.RootElement.GetProperty("expenses").EnumerateArray().Should().NotBeNullOrEmpty();
    }
}
