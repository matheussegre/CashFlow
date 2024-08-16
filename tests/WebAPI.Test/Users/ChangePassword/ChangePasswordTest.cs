using CashFlow.Communication.Requests;
using CashFlow.Exception;
using CommonTestUtilities.Requests;
using FluentAssertions;
using System.Net;
using System.Text.Json;
using WebAPI.Test.InlineData;

namespace WebAPI.Test.Users.ChangePassword;
public class ChangePasswordTest : CashFlowClassFixture
{
    private const string ROUTE = "api/users/change-password";

    private readonly string _token;
    private readonly string _password;
    private readonly string _email;

    public ChangePasswordTest(CustomWebApplicationFactory customWebApplication) : base(customWebApplication)
    {
        _token = customWebApplication.User_Team_Member.GetToken();
        _password = customWebApplication.User_Team_Member.GetPassword();
        _email = customWebApplication.User_Team_Member.GetEmail();
    }

    [Fact]
    public async Task Success()
    {
        var request = RequestChangePasswordJsonBuilder.Build();
        request.Password = _password;

        var response = await DoPut(ROUTE, request, _token);

        response.StatusCode.Should().Be(HttpStatusCode.NoContent);

        var loginRequest = new RequestLoginJson()
        {
            Email = _email,
            Password = _password,
        };

        response = await DoPost("api/login", loginRequest);
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

        loginRequest.Password = request.NewPassword;

        response = await DoPost("api/login", loginRequest);
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Theory]
    [ClassData(typeof(CultureInlineDataTest))]
    public async Task Error_Password_Different_Current_Password(string culture)
    {
        var request = RequestChangePasswordJsonBuilder.Build();

        var response = await DoPut(ROUTE, request, _token, culture);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        await using var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        var errors = responseData.RootElement.GetProperty("errorMessages").EnumerateArray();

        var message = ResourceErrorMessages.ResourceManager.GetString("INVALID_PASSWORD", new System.Globalization.CultureInfo(culture));

        errors.Should().HaveCount(1).And.Contain(c => c.GetString()!.Equals(message));


    }
}
