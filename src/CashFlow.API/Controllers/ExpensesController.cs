﻿using CashFlow.Application.UseCases.Expenses.Register;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Exception.ExceptionsBase;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ExpensesController : ControllerBase
{
    [HttpPost]
    public IActionResult Register(
        [FromServices] IRegisterExpenseUseCase useCase, 
        [FromBody] RequestExpenseJson request)
    {
        var response = useCase.Execute(request);

        return Created(string.Empty, response);
    }
}
