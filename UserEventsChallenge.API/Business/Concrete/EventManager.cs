using AutoMapper;
using UserEventsChallenge.API.Business.Abstract;
using UserEventsChallenge.API.Business.Caching;
using UserEventsChallenge.API.DataAccess.Concrete;
using UserEventsChallenge.API.Entities.Concrete;
using UserEventsChallenge.API.Entities.Dtos;
using UserEventsChallenge.API.Entities.Exceptions;
using UserEventsChallenge.API.Entities.Paging;

namespace UserEventsChallenge.API.Business.Concrete;

public class EventManager : IEventManager
{
    private readonly IEventDal _eventDal;
    private readonly ICacheManager _cacheManager;
    private readonly IMapper _mapper;

    public EventManager(IEventDal eventDal, ICacheManager cacheManager, IMapper mapper)
    {
        _eventDal = eventDal;
        _cacheManager = cacheManager;
        _mapper = mapper;
    }

    public async Task AddAsync(AddEventDto addEventDto)
    {
        CheckTimeZoneId(addEventDto.TimeZoneId);

        var @event = _mapper.Map<Event>(addEventDto);

        _cacheManager.RemoveByPattern($"Events.GetListAsync?UserId={@event.UserId}");

        await _eventDal.AddAsync(@event);
    }

    public async Task DeleteAsync(int id)
    {
        if (id == 0)
            throw new BusinessException("Invalid Id");

        var @event = await _eventDal.GetAsync(e => e.Id == id);

        _cacheManager.RemoveByPattern($"Events.GetListAsync?UserId={@event.UserId}");

        await _eventDal.DeleteAsync(@event);
    }

    public async Task<EventDto> GetAsync(int id)
    {
        if (id == 0)
            throw new BusinessException("Invalid Id");

        var @event = await _eventDal.GetAsync(e => e.Id == id);

        var result = _mapper.Map<EventDto>(@event);

        return result;
    }

    public async Task<IPaginate<EventDto>> GetListAsync(PageRequest pageRequest, int userId)
    {
        string cacheKey = $"Events.GetListAsync?UserId={userId}&Page={pageRequest.Page}&Size={pageRequest.PageSize}";
        if (_cacheManager.IsAdd(cacheKey))
            return _cacheManager.Get<IPaginate<EventDto>>(cacheKey);

        var @event = await _eventDal.GetListAsync(predicate: e => e.UserId == userId, size: pageRequest.PageSize, index: pageRequest.Page);

        var result = _mapper.Map<Paginate<EventDto>>(@event);

        _cacheManager.Add(cacheKey, result);

        return result;
    }

    public async Task UpdateAsync(UpdateEventDto updateEventDto)
    {
        if (updateEventDto.Id == 0)
            throw new BusinessException("InvalidId");

        CheckTimeZoneId(updateEventDto.TimeZoneId);

        var @event = _mapper.Map<Event>(updateEventDto);

        _cacheManager.RemoveByPattern($"Events.GetListAsync?UserId={@event.UserId}");

        await _eventDal.UpdateAsync(@event);
    }

    private void CheckTimeZoneId(string timeZoneId)
    {
        try
        {
            var timeZoneIds = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
        }
        catch (TimeZoneNotFoundException)
        {
            throw new BusinessException("InvalidTimeZoneId");
        }
        catch (Exception ex)
        {
            throw new BusinessException(ex.Message);
        }
    }
}
