namespace Queries.Members.Handlers.SearchMembers;

public class AutocompleteMember
{
    public AutocompleteMember(string name, Guid? memberId)
    {
        Name = name;
        MemberId = memberId;
    }
    public string Name { get; set; } = string.Empty;
    public Guid? MemberId { get; set; }
};
