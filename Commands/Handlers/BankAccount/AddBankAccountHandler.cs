using System;
using System.Threading;
using System.Threading.Tasks;

using Domain.Interfaces;

using MediatR;

namespace Commands.Handlers.BankAccount
{
    public class AddBankAccountHandler : IRequestHandler<AddBankAccountCommand, Guid>
    {
        private readonly IBankAccountRepository _bankAccountRepository;

        public AddBankAccountHandler(IBankAccountRepository bankAccountRepository)
        {
            _bankAccountRepository = bankAccountRepository;
        }

        public async Task<Guid> Handle(AddBankAccountCommand request, CancellationToken ct)
        {
            var newAccount = new Domain.BankAccount(request.Name, request.AccountNumber);
            _bankAccountRepository.Add(newAccount);
            await _bankAccountRepository.SaveAsync(ct);

            return newAccount.Id;
        }
    }
}