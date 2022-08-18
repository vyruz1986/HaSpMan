using AutoMapper;

using Commands.Handlers.Member.AddMember;

using Queries.Members.ViewModels;

using Types;

using Web.Models;

namespace Web.MapperProfiles;

public class MemberProfile : Profile
{
    public MemberProfile()
    {
        CreateMap<MemberForm, AddMemberCommand>()
           .ForCtorParam(nameof(AddMemberCommand.Address), o => o.MapFrom(src => src))
           .ForMember(m => m.Address, o => o.MapFrom(src => src));


        CreateMap<MemberForm, Address>();

        CreateMap<MemberDetail, MemberForm>()
            .ForMember(m => m.MembershipExpiryDate, o => o.MapFrom(src => ToDateTime(src.MembershipExpiryDate)))
            .IncludeMembers(m => m.Address);

        CreateMap<Address, MemberForm>()
            .ForMember(m => m.FirstName, o => o.Ignore())
            .ForMember(m => m.LastName, o => o.Ignore())
            .ForMember(m => m.MembershipExpiryDate, o => o.Ignore())
            .ForMember(m => m.MembershipFee, o => o.Ignore())
            .ForMember(m => m.PhoneNumber, o => o.Ignore())
            .ForMember(m => m.Email, o => o.Ignore());
    }
    private static DateTime? ToDateTime(DateTimeOffset? dateTimeOffset)
    {
        if (dateTimeOffset == null)
            return null;

        return dateTimeOffset!.Value.DateTime;
    }
}