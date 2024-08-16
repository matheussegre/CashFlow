using CommonTestUtilities.Login;
using FluentAssertions;
using System.Net;

namespace WebAPI.Test.Users.Delete;
public class DeleteUserTest : CashFlowClassFixture
{
    private const string ROUTE = "api/users";

    private readonly string _token;

    public DeleteUserTest(CustomWebApplicationFactory customWebApplication) : base(customWebApplication)
    {
        _token = customWebApplication.User_Team_Member.GetToken();
    }

    [Fact]
    public async Task Success()
    {
        var result = await DoDelete(ROUTE, _token);

        result.StatusCode.Should().Be(HttpStatusCode.NoContent);

        var request = RequestLoginJsonBuilder.Build();

        result = await DoPost("api/login", request, _token);

        result.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

}
