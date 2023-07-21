using UserEventsChallenge.API.Entities.Dtos;
using UserEventsChallenge.API.Entities.Paging;

namespace UserEventsChallenge.API.Business.Abstract;

public interface IEventParticipantManager
{
    Task AddParticipantAsync(AddEventParticipantDto addEventParticipantDto);

    Task<IPaginate<EventParticipantDto>> GetParticipantAsync(PageRequest pageRequest,int eventId);

    Task<bool> IsParticipant(int userId, int eventId);
}
