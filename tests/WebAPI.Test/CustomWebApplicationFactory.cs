﻿using CashFlow.Domain.Entities;
using CashFlow.Domain.Enums;
using CashFlow.Domain.Security.Criptography;
using CashFlow.Domain.Security.Tokens;
using CashFlow.Infrastructure.DataAccess;
using CommonTestUtilities.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebAPI.Test.Resources;

namespace WebAPI.Test;
public class CustomWebApplicationFactory: WebApplicationFactory<Program>
{
    public ExpenseIdentityManager Expense_Admin { get; private set; } = default!;
    public ExpenseIdentityManager Expense_TeamMember { get; private set; } = default!;
    public UserIdentityManager User_Admin { get; private set; } = default!;
    public UserIdentityManager User_Team_Member { get; private set; } = default!;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Tests").ConfigureServices(services =>
        {
            var provider = services.AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();

            services.AddDbContext<CashFlowDbContext>(config =>
            {
                config.UseInMemoryDatabase("InMemoryDbForTesting");
                config.UseInternalServiceProvider(provider);
            });

            var scope = services.BuildServiceProvider().CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<CashFlowDbContext>();
            var passwordEncipter = scope.ServiceProvider.GetRequiredService<IPasswordEncripter>();
            var accessTokenGenerator = scope.ServiceProvider.GetRequiredService<IAccessTokenGenerator>();

            StartDataBase(dbContext, passwordEncipter, accessTokenGenerator);

           
        });
    }

    private void StartDataBase(CashFlowDbContext dbContext, IPasswordEncripter passwordEncripter, IAccessTokenGenerator accessTokenGenerator)
    {
        var userTeamMember = AddUserTemMember(dbContext, passwordEncripter, accessTokenGenerator);
        var expenseTeamMember = AddExpenses(dbContext, userTeamMember, expenseId: 1);
        Expense_TeamMember = new ExpenseIdentityManager(expenseTeamMember);

        var userAdmin = AddUserAdmin(dbContext, passwordEncripter, accessTokenGenerator);
        var expenseAdmin = AddExpenses(dbContext, userAdmin, expenseId: 2);
        Expense_Admin = new ExpenseIdentityManager(expenseAdmin);

        dbContext.SaveChanges();
    }

    private User AddUserAdmin(CashFlowDbContext dbContext, IPasswordEncripter passwordEncripter, IAccessTokenGenerator accessTokenGenerator)
    {
        var user = UserBuilder.Build(Roles.ADMIN);
        user.Id = 2;
        var password = user.Password;

        user.Password = passwordEncripter.ExecuteCriptography(user.Password);
        dbContext.Users.Add(user);

        var token = accessTokenGenerator.Generate(user);

        User_Admin = new UserIdentityManager(user, password, token);

        return user;
    }

    private User AddUserTemMember(CashFlowDbContext dbContext, IPasswordEncripter passwordEncripter, IAccessTokenGenerator accessTokenGenerator)
    {
        var user = UserBuilder.Build();
        user.Id = 1;

        var password = user.Password;

        user.Password = passwordEncripter.ExecuteCriptography(user.Password);
        dbContext.Users.Add(user);

        var token = accessTokenGenerator.Generate(user);
        
        User_Team_Member = new UserIdentityManager(user, password, token);
    
        return user;
    }

    private Expense AddExpenses(CashFlowDbContext dbContext, User user, long expenseId)
    {
        var expense = ExpenseBuilder.Build(user);
        expense.Id = expenseId;

        dbContext.Expenses.Add(expense);


        return expense;
    }
}
