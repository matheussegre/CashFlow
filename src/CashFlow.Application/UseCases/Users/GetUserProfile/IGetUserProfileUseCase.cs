using CashFlow.Communication.Responses;

namespace CashFlow.Application.UseCases.Users.GetUserProfile;
public interface IGetUserProfileUseCase
{
    Task<ResponseUserProfileJson> Execute();
}
