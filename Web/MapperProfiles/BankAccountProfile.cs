using AutoMapper;

using Commands.Handlers.BankAccount.AddBankAccount;

using Domain;

using Persistence.Views;

using Queries.BankAccounts;

using Web.Models;

namespace Web.MapperProfiles;

public class BankAccountProfile : Profile
{
    public BankAccountProfile()
    {
        CreateMap<BankAccountForm, AddBankAccountCommand>()
            .ForCtorParam(nameof(AddBankAccountCommand.Name), o => o.MapFrom(src => src.Name))
            .ForCtorParam(nameof(AddBankAccountCommand.AccountNumber), o => o.MapFrom(src => src.AccountNumber));

        CreateMap<BankAccountDetail, BankAccountForm>();
        CreateMap<BankAccount, BankAccountDetail>()
            .ForCtorParam(nameof(BankAccountDetail.Id), o => o.MapFrom(src => src.Id))
            .ForCtorParam(nameof(BankAccountDetail.AccountNumber), o => o.MapFrom(src => src.AccountNumber))
            .ForCtorParam(nameof(BankAccountDetail.Name), o => o.MapFrom(src => src.Name));
        CreateMap<BankAccountsWithTotals, BankAccountDetailWithTotal>()
            .ForCtorParam(nameof(BankAccountDetailWithTotal.AccountNumber), o => o.MapFrom(src => src.Account.AccountNumber))
            .ForCtorParam(nameof(BankAccountDetailWithTotal.Id), o => o.MapFrom(src => src.BankAccountId))
            .ForCtorParam(nameof(BankAccountDetailWithTotal.Name), o => o.MapFrom(src => src.Account.Name))
            .ForCtorParam(nameof(BankAccountDetailWithTotal.TotalAmount), o => o.MapFrom(src => src.Total));
    }
}