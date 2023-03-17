using System.ComponentModel.DataAnnotations;

namespace Queries.Members.Handlers.AutocompleteMember;

public record AutocompleteCounterparty(
    [property: Required(AllowEmptyStrings = false, ErrorMessage = "The name of the counterparty is required"), StringLength(120)] string Name,
    Guid? MemberId);
