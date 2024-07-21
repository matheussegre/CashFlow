using CashFlow.Application.UseCases.Expenses.Reports.Excel;
using CashFlow.Application.UseCases.Expenses.Reports.PDF;
using CashFlow.Communication.Requests;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace CashFlow.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ReportController : ControllerBase
{
    [HttpGet("excel")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]

    public async Task<IActionResult> GetExcel(
        [FromServices] IGenerateExpensesReportUseCase useCase,
        [FromHeader] DateTime month)
    {
        byte[] file = await useCase.Execute(DateOnly.FromDateTime(month));

        if (file.Length > 0) return File(file, MediaTypeNames.Application.Octet, "report.xlsx");

        return NoContent();
    }

    [HttpGet("pdf")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetPDF(
        [FromServices] IGenerateExpensesReportPdfUseCase useCase,
        [FromQuery] DateTime month)
    {
        byte[] file = await useCase.Execute(DateOnly.FromDateTime(month));

        if (file.Length > 0) return File(file, MediaTypeNames.Application.Pdf, "report.pdf");

        return NoContent();
    }
}
