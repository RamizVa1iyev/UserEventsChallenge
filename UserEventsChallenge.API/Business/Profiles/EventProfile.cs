using AutoMapper;
using UserEventsChallenge.API.Entities.Concrete;
using UserEventsChallenge.API.Entities.Dtos;
using UserEventsChallenge.API.Entities.Paging;

namespace UserEventsChallenge.API.Business.Profiles
{
    public class EventProfile:Profile
    {
        public EventProfile()
        {
            CreateMap<AddEventDto, Event>();
            CreateMap<UpdateEventDto, Event>();

            CreateMap<AddEventParticipantDto, EventParticipant>();

            CreateMap<Event,EventDto>().ForMember(e=>e.TimeZone,opt=>opt.MapFrom(e=>TimeZoneInfo.FindSystemTimeZoneById(e.TimeZoneId)));
            CreateMap<IPaginate<Event>, Paginate<EventDto>>();

            CreateMap<EventParticipant, EventParticipantDto>();
            CreateMap<IPaginate<EventParticipant>, Paginate<EventParticipantDto>>();
        }
    }
}
