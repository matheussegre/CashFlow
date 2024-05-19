using CashFlow.Domain.Enums;
using CashFlow.Domain.Reports;

namespace CashFlow.Domain.Extensions;
public static class PaymentMethodExtensions
{
    public static string PaymentMethodToString(this PaymentMethod paymentMethod)
    {
        return paymentMethod switch
        {
            PaymentMethod.CASH => ResourceReportGenerationMessages.CASH,
            PaymentMethod.CREDIT_CARD => ResourceReportGenerationMessages.CREDIT_CARD,
            PaymentMethod.DEBIT_CARD => ResourceReportGenerationMessages.DEBIT_CARD,
            PaymentMethod.ELETRONIC_TRANSFER => ResourceReportGenerationMessages.ELETRONIC_TRANSFER,
            _ => string.Empty
        };
    }
}
