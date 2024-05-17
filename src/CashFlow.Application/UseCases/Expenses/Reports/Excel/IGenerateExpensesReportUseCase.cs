namespace CashFlow.Application.UseCases.Expenses.Reports.Excel;
public interface IGenerateExpensesReportUseCase
{
    Task<byte[]> Execute(DateOnly month);
}
