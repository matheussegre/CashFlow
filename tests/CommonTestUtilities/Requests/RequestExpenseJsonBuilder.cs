using Bogus;
using CashFlow.Communication.Enums;
using CashFlow.Communication.Requests;

namespace CommonTestUtilities.Requests;
public class RequestExpenseJsonBuilder
{
    public static RequestExpenseJson Build()
    {
        return new Faker<RequestExpenseJson>()
            .RuleFor(request => request.Title, faker => faker.Commerce.ProductName())
            .RuleFor(request => request.Description, faker => faker.Commerce.ProductDescription())
            .RuleFor(request => request.Date, faker => faker.Date.Past())
            .RuleFor(request => request.PaymentsMethod, faker => faker.PickRandom<PaymentsMethods>())
            .RuleFor(request => request.Amount, faker => faker.Random.Decimal(min: 1, max: 1000));
        
        //var faker = new Faker();
        //var order = new RequestExpenseJson
        //{
        //    Title = faker.Commerce.Product(),
        //    Date = faker.Date.Past(),

        //};
    }
}
