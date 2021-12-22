using MediatR;

namespace Queries.Members.Handlers.AutocompleteMember;

public record AutocompleteCounterpartyQuery(string SearchString, bool IsMemberSearch) : IRequest<AutocompleteCounterpartyResponse>;
public record AutocompleteCounterpartyResponse(IReadOnlyList<AutocompleteCounterparty> Counterparties);
