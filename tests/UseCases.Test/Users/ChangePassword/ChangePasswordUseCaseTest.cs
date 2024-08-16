using CashFlow.Application.UseCases.Login;
using CashFlow.Application.UseCases.Users.ChangePassword;
using CashFlow.Domain.Entities;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionsBase;
using CommonTestUtilities.Criptography;
using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using CommonTestUtilities.Token;
using FluentAssertions;

namespace UseCases.Test.Users.ChangePassword;
public class ChangePasswordUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var user = UserBuilder.Build();

        var request = RequestChangePasswordJsonBuilder.Build();

        var useCase = CreateUseCase(user, request.Password);

        var act = async () => await useCase.Execute(request);

        await act.Should().NotThrowAsync();
    }

    [Fact]
    public async Task Error_NewPassword_Empty()
    {
        var user = UserBuilder.Build();

        var request = RequestChangePasswordJsonBuilder.Build();
        request.NewPassword = string.Empty;

        var useCase = CreateUseCase(user, request.Password);

        var act = async () => await useCase.Execute(request);

        var result = await act.Should().ThrowAsync<ErrorOnValidationException>();

        result.Where(e => e.GetErrors().Count.Equals(1) && e.GetErrors().Contains(ResourceErrorMessages.INVALID_PASSWORD));
    }

    [Fact]  
    public async Task Error_CurrentPassword_Diferent()
    {
        var user = UserBuilder.Build();

        var request = RequestChangePasswordJsonBuilder.Build();

        var useCase = CreateUseCase(user);

        var act = async () => await useCase.Execute(request);

        var result = await act.Should().ThrowAsync<ErrorOnValidationException>();

        result.Where(e => e.GetErrors().Count.Equals(1) && e.GetErrors().Contains(ResourceErrorMessages.INVALID_PASSWORD));
    }

    private ChangePasswordUseCase CreateUseCase(User user, string? password = null)
    {
        var loggedUser = LoggedUserBuilder.Build(user);
        var passwordEncripter = new PasswordEncrypterBuilder().Verify(password).Build();
        var repository =  UserUpdateOnlyRepositoryBuilder.Build(user);
        var unitOfWork = UnitOfWorkBuilder.Build();

        return new ChangePasswordUseCase(loggedUser, repository, unitOfWork, passwordEncripter);
    }
}
