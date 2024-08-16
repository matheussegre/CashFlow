﻿
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Users;
using CashFlow.Domain.Services.LoggedUser;

namespace CashFlow.Application.UseCases.Users.Delete;
public class DeleteUserUseCase : IDeleteUserUseCase
{
    private readonly ILoggedUser _loggedUser;
    private readonly IUserWriteOnlyRepostiory _repository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteUserUseCase(ILoggedUser loggedUser, IUserWriteOnlyRepostiory repository, IUnitOfWork unitOfWork)
    {
        _loggedUser = loggedUser;
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute()
    {
        var user = await _loggedUser.Get();

        await _repository.Delete(user);

        await _unitOfWork.Commit();
    }
}
