using AutoMapper;

using Commands;

using Types;

using Web.Models;

namespace Web.MapperProfiles
{
    public class MemberProfile : Profile
    {
        public MemberProfile()
        {
            CreateMap<NewMember, AddMemberCommand>()
               .ForCtorParam(nameof(AddMemberCommand.Address), o => o.MapFrom(src => src))
               .ForMember(m => m.Address, o => o.MapFrom(src => src));

            CreateMap<NewMember, Address>();
        }
    }
}