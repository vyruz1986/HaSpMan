using AutoMapper;

using Commands.Handlers.BankAccount.AddBankAccount;

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
        CreateMap<BankAccountsWithTotals, BankAccountDetail>()
            .ForCtorParam(nameof(BankAccountDetail.AccountNumber), o => o.MapFrom(src => src.Account.AccountNumber))
            .ForCtorParam(nameof(BankAccountDetail.Id), o => o.MapFrom(src => src.BankAccountId))
            .ForCtorParam(nameof(BankAccountDetail.Name), o => o.MapFrom(src => src.Account.Name))
            .ForCtorParam(nameof(BankAccountDetail.TotalAmount), o => o.MapFrom(src => src.Total));
    }
}