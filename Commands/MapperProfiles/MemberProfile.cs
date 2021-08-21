using AutoMapper;

using Domain;

namespace Commands.MapperProfiles
{
    public class MemberProfile : Profile
    {
        public MemberProfile()
        {
            CreateMap<AddMemberCommand, Member>();
        }
    }
}