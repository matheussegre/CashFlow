﻿using CashFlow.Application.UseCases.Users;
using CashFlow.Application.UseCases.Users.Register;
using CashFlow.Communication.Requests;
using CommonTestUtilities.Requests;
using FluentAssertions;
using FluentValidation;
using Xunit;

namespace Validators.Test.Users;
public class PaswordValidatorTest
{
    [Theory]
    [InlineData("")]
    [InlineData("          ")]
    [InlineData(null)]
    [InlineData("a")]
    [InlineData("aa")]
    [InlineData("aaa")]
    [InlineData("aaaa")]
    [InlineData("aaaaa")]
    [InlineData("aaaaaa")]
    [InlineData("aaaaaaa")]
    [InlineData("aaaaaaaa")]
    [InlineData("AAAAAAAA")]
    [InlineData("Aaaaaaaa")]
    [InlineData("Aaaaaaa1")]
    public void Error_Password_Invalid(string password)
    {
        //Arrange
        var validator = new PasswordValidator<RequestRegisterUserJson>();

        //Act
        var result = validator.IsValid(
            new ValidationContext<RequestRegisterUserJson>(new RequestRegisterUserJson()), 
            password
            );

        //Assert
        result.Should().BeFalse();
    }
}
