using System;
using System.Collections.Generic;

using MediatR;

namespace Queries.Members.Handlers.GetBankAccountInfos
{
    public record GetBankAccountInfos() : IRequest<IReadOnlyList<BankAccountInfo>>;

    public class BankAccountInfo
    {
        public BankAccountInfo(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public Guid Id { get; private set; }

        public string Name { get; private set; }

        public override string ToString()
        {
            return Name;
        }
    }
}