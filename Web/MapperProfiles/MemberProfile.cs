using System;

using AutoMapper;

using Commands;

using Queries.Members.ViewModels;

using Types;

using Web.Models;

namespace Web.MapperProfiles
{
    public class MemberProfile : Profile
    {
        public MemberProfile()
        {
            CreateMap<MemberForm, AddMemberCommand>()
               .ForCtorParam(nameof(AddMemberCommand.Address), o => o.MapFrom(src => src))
               .ForMember(m => m.Address, o => o.MapFrom(src => src));


            CreateMap<MemberForm, Address>();

            CreateMap<MemberDetail, MemberForm>()
                .ForMember(m => m.MembershipExpiryDate, o => o.MapFrom(src => src.MembershipExpiryDate.DateTime))
                .IncludeMembers(m => m.Address);

            CreateMap<Address, MemberForm>();
        }
    }
}