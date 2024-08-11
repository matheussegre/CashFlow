using AutoMapper;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Domain.Services.LoggedUser;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionsBase;

namespace CashFlow.Application.UseCases.Expenses.GetById;
internal class GetExpenseByIdUseCase : IGetExpenseByIdUseCase
{
    private readonly IExpensesReadOnlyRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILoggedUser _loggedUser;
    public GetExpenseByIdUseCase(ILoggedUser loggedUser,
        IExpensesReadOnlyRepository repository, IMapper mapper)
    {
        _loggedUser = loggedUser;
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<ResponseExpenseJson> Execute(long id)
    {
        var loggedUser = await _loggedUser.Get();

        var result = await _repository.GetById(loggedUser,id);

        if (result is null) throw new NotFoundException(ResourceErrorMessages.EXPENSE_NOT_FOUND);

        return _mapper.Map<ResponseExpenseJson>(result);
    }
}
