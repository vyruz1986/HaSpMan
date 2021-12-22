using AutoMapper;

using Domain;

using Queries.Members.ViewModels;

namespace Queries.MapperProfiles;

public class MemberProfile : Profile
{
    public MemberProfile()
    {
        CreateMap<Member, MemberSummary>()
            .ForCtorParam(nameof(MemberSummary.Address), o => o.MapFrom(src => src.Address.ToString()));

        CreateMap<Member, MemberDetail>();
    }
}
