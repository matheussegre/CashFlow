using CashFlow.Domain.Enums;
using CashFlow.Domain.PaymentMethods;
using CashFlow.Domain.Reports;
using CashFlow.Domain.Repositories.Expenses;
using ClosedXML.Excel;

namespace CashFlow.Application.UseCases.Expenses.Reports.Excel;
public class GenerateExpensesReportUseCase : IGenerateExpensesReportUseCase
{
    private const string CURRENCY_SYMBOL = "$";
    private readonly IExpensesReadOnlyRepository _repository;

    public GenerateExpensesReportUseCase(IExpensesReadOnlyRepository repository)
    {
        _repository = repository;
    }

    public async Task<byte[]> Execute(DateOnly month)
    {
        var expenses = await _repository.FilterByMonth(month);

        if (expenses.Count == 0) return [];

        using var workbook = new XLWorkbook();

        workbook.Author = "Matheus Segre";
        workbook.Style.Font.FontSize = 14;
        workbook.Style.Font.FontName = "Arial";

        var worksheet = workbook.Worksheets.Add(month.ToString("Y"));

        InsertHeader(worksheet);

        var row = 2;
        foreach (var expense in expenses) 
        {
            worksheet.Cell($"A{row}").Value = expense.Title;        
            worksheet.Cell($"B{row}").Value = expense.Date;        
            worksheet.Cell($"C{row}").Value = ConvertPaymentMethod(expense.PaymentMethod);        
            
            worksheet.Cell($"D{row}").Value = expense.Amount;
            worksheet.Cell($"D{row}").Style.NumberFormat.Format = $"-{CURRENCY_SYMBOL} #,##0,00";
            
            worksheet.Cell($"E{row}").Value = expense.Description;

            row++;
        }

        worksheet.Columns().AdjustToContents();

        var file = new MemoryStream();
        workbook.SaveAs(file);

        return file.ToArray();
    }

    private string ConvertPaymentMethod(PaymentMethod payment)
    {
        return payment switch
        {
            PaymentMethod.CASH => ResourcePaymentMethods.CASH,
            PaymentMethod.CREDIT_CARD => ResourcePaymentMethods.CREDIT_CARD,
            PaymentMethod.DEBIT_CARD => ResourcePaymentMethods.DEBIT_CARD,
            PaymentMethod.ELETRONIC_TRANSFER => ResourcePaymentMethods.ELETRONIC_TRANSFER,
            _ => string.Empty
        };
    }

    private void InsertHeader(IXLWorksheet worksheet)
    {
        worksheet.Cell("A1").Value = ResourceReportGenerationMessages.TITLE;
        worksheet.Cell("B1").Value = ResourceReportGenerationMessages.DATE;
        worksheet.Cell("C1").Value = ResourceReportGenerationMessages.PAYMENT_METHOD;
        worksheet.Cell("D1").Value = ResourceReportGenerationMessages.AMOUNT;
        worksheet.Cell("E1").Value = ResourceReportGenerationMessages.DESCRIPTION;

        worksheet.Cells("A1:E1").Style.Font.Bold = true;
        worksheet.Cells("A1:E1").Style.Fill.BackgroundColor = XLColor.FromHtml("#82E0AA");

        worksheet.Cell("Ä1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        worksheet.Cell("B1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        worksheet.Cell("C1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        worksheet.Cell("E1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        
        worksheet.Cell("D1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);
    }
}
