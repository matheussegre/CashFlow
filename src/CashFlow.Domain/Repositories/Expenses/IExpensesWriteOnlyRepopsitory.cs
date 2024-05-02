using CashFlow.Domain.Entities;

namespace CashFlow.Domain.Repositories.Expenses;
public interface IExpensesWriteOnlyRepopsitory
{
    Task AddExpense(Expense expense);
}
