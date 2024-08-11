using CashFlow.Domain.Entities;

namespace CashFlow.Domain.Repositories.Expenses;
public interface IExpensesWriteOnlyRepository
{
    Task AddExpense(Expense expense);
    /// <summary>
    /// This is a context exemple
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task Delete(long id);
}
