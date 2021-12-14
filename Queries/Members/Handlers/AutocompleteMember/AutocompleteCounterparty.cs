using System;

namespace Queries.Members.Handlers.AutocompleteMember
{
    public class AutocompleteCounterparty
    {
        public AutocompleteCounterparty(string name, Guid? memberId)
        {
            Name = name;
            MemberId = memberId;
        }
        public string Name { get; set; }
        public Guid? MemberId { get; set; }
    };
}