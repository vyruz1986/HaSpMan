using Domain;

using Queries.Members.ViewModels;

namespace Queries.MapperProfiles;

public class MemberProfile : Profile
{
    public MemberProfile()
    {
        CreateMap<Member, MemberSummary>()
            .ForCtorParam(nameof(MemberSummary.Address), o => o.MapFrom(src => src.Address.ToString()))
            .ForMember(x => x.IsActive, o => o.MapFrom(x => x.IsActive()));

        CreateMap<Member, MemberDetail>();
    }
}
