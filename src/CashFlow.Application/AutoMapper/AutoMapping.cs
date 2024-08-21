using AutoMapper;
using CashFlow.Communication.Enums;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;

namespace CashFlow.Application.AutoMapper;
public class AutoMapping : Profile
{
    public AutoMapping()
    {
        RequestToEntity();
        EntityToResponse();
    }

    private void RequestToEntity()
    {
        CreateMap<RequestRegisterUserJson, User>()
                .ForMember(entity => entity.Password, config => config.Ignore());

        CreateMap<RequestExpenseJson, Expense>().ForMember(dest => dest.Tags, 
            config => config.MapFrom(source => source.Tags.Distinct()));


        CreateMap<Tags, Tag>().ForMember(dest => dest.Tags, config => config.MapFrom(source => source));
    }

    private void EntityToResponse()
    {
        CreateMap<Expense,ResponseExpenseJson>().ForMember(dest => dest.Tags, 
            config => config.MapFrom(source => source.Tags.Select(tag => tag.Tags)));

        CreateMap<Expense,ResponseRegisteredExpenseJson>();
        CreateMap<Expense,ResponseShortExpenseJson>();

        CreateMap<User,ResponseUserProfileJson>();
    }
}
