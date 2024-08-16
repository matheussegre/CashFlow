using FluentAssertions;
using System.Net;
using System.Net.Mime;

namespace WebAPI.Test.Expenses.Reports;
public class GenerateExpensesReportsTest : CashFlowClassFixture
{
    private const string ROUTE = "api/report";

    private readonly string _adminToken;
    private readonly string _teamMemberToken;
    private readonly DateTime _expenseDate;

    public GenerateExpensesReportsTest(CustomWebApplicationFactory webApplicationFactory) : base(webApplicationFactory)
    {
        _adminToken = webApplicationFactory.User_Admin.GetToken();
        _teamMemberToken = webApplicationFactory.User_Team_Member.GetToken();
        _expenseDate = webApplicationFactory.Expense_Admin.GetDate();
    }

    [Fact]
    public async Task Success_PDF()
    {
        var result = await DoGet($"{ROUTE}/pdf?month={_expenseDate:yyyy-MM}", _adminToken);

        result.StatusCode.Should().Be(HttpStatusCode.OK);

        result.Content.Headers.ContentType.Should().NotBeNull();
        result.Content.Headers.ContentType.MediaType.Should().Be(MediaTypeNames.Application.Pdf);
    }

    [Fact]
    public async Task Success_Excel()
    {
        var result = await DoGet($"{ROUTE}/excel?month={_expenseDate:yyyy-MM}", _adminToken);

        result.StatusCode.Should().Be(HttpStatusCode.OK);

        result.Content.Headers.ContentType.Should().NotBeNull();
        result.Content.Headers.ContentType.MediaType.Should().Be(MediaTypeNames.Application.Octet);
    }

    [Fact]
    public async Task Error_Forbidden_User_Pdf()
    {
        var result = await DoGet($"{ROUTE}/pdf?month={_expenseDate:Y}", _teamMemberToken);

        result.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task Error_Forbidden_User_Excel()
    {
        var result = await DoGet($"{ROUTE}/excel?month={_expenseDate:Y}", _teamMemberToken);

        result.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }
}
