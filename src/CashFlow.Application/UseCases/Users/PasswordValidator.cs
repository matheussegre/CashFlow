using CashFlow.Exception;
using FluentValidation;
using FluentValidation.Validators;
using System.Text.RegularExpressions;

namespace CashFlow.Application.UseCases.Users;
public partial class PasswordValidator<T> : PropertyValidator<T, string>
{
    private const string ERROR_MESSAGE_KEY = "ErrorMessage";
    public override string Name => "PasswordValidator";

    protected override string GetDefaultMessageTemplate(string errorCode)
    {
        return $"{{{ERROR_MESSAGE_KEY}}}";
    }

    public override bool IsValid(ValidationContext<T> context, string password)
    {
        if (string.IsNullOrWhiteSpace(password)) return ValidationMessage(context);

        if (password.Length < 8) return ValidationMessage(context);

        if (UpperCaseLetter().IsMatch(password) is false) return ValidationMessage(context);

        if (LowerCaseLetter().IsMatch(password) is false) return ValidationMessage(context);

        if (Numbers().IsMatch(password) is false) return ValidationMessage(context);

        if (SpecialSymbols().IsMatch(password) is false) return ValidationMessage(context);

        return true;
    }

    private bool ValidationMessage(ValidationContext<T> context)
    {
        context.MessageFormatter.AppendArgument(
               ERROR_MESSAGE_KEY,
               ResourceErrorMessages.INVALID_PASSWORD
           );

        return false;
    }

    [GeneratedRegex(@"[A-Z]+")]
    private static partial Regex UpperCaseLetter();

    [GeneratedRegex(@"[a-z]+")]
    private static partial Regex LowerCaseLetter();
    
    [GeneratedRegex(@"[0-9]+")]
    private static partial Regex Numbers();
    
    [GeneratedRegex(@"[\!\?\*\.\@\&\%\$\#]+")]
    private static partial Regex SpecialSymbols();
}
