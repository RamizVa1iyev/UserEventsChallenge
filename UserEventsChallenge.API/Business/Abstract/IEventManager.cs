using UserEventsChallenge.API.Entities.Concrete;
using UserEventsChallenge.API.Entities.Dtos;
using UserEventsChallenge.API.Entities.Paging;

namespace UserEventsChallenge.API.Business.Abstract;

public interface IEventManager
{
    Task<IPaginate<EventDto>> GetListAsync(PageRequest pageRequest,int userId);
    Task<EventDto> GetAsync(int id);

    Task AddAsync(AddEventDto addEventDto);
    Task UpdateAsync(UpdateEventDto updateEventDto);
    Task DeleteAsync(int id);
}
