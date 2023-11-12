using ApplicationCore.DTO.Meetup;
using ApplicationCore.Entities;
using AutoMapper;

namespace ApplicationCore.MapperProfiles;

/// <summary>
/// Profile to create maps for meetup dtos
/// </summary>
public class MeetupProfile : Profile
{
    public MeetupProfile()
    {
        CreateMap<MeetupAddDto, Meetup>();

        // Ignore null members
        CreateMap<MeetupUpdateDto, Meetup>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

    }
}
