using AutoMapper;

using Commands.Handlers.BankAccount;

using Web.Models;

namespace Web.MapperProfiles;

public class BankAccountProfile : Profile
{
    public BankAccountProfile()
    {
        CreateMap<BankAccountForm, AddBankAccountCommand>()
            .ForCtorParam(nameof(AddBankAccountCommand.Name), o => o.MapFrom(src => src.Name))
            .ForCtorParam(nameof(AddBankAccountCommand.AccountNumber), o => o.MapFrom(src => src.AccountNumber));

    }
}