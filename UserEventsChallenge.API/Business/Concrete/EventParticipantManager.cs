using AutoMapper;
using UserEventsChallenge.API.Business.Abstract;
using UserEventsChallenge.API.Business.Caching;
using UserEventsChallenge.API.DataAccess.Abstract;
using UserEventsChallenge.API.Entities.Concrete;
using UserEventsChallenge.API.Entities.Dtos;
using UserEventsChallenge.API.Entities.Exceptions;
using UserEventsChallenge.API.Entities.Paging;

namespace UserEventsChallenge.API.Business.Concrete
{
    public class EventParticipantManager : IEventParticipantManager
    {
        private readonly IEventParticipantDal _eventParticipantDal;
        private readonly ICacheManager _cacheManager;
        private readonly IMapper _mapper;

        public EventParticipantManager(IEventParticipantDal eventParticipantDal, IMapper mapper, ICacheManager cacheManager)
        {
            _eventParticipantDal = eventParticipantDal;
            _mapper = mapper;
            _cacheManager = cacheManager;
        }

        public async Task AddParticipantAsync(AddEventParticipantDto addEventParticipantDto)
        {
            var _participant = await _eventParticipantDal.GetAsync(p => p.UserId == addEventParticipantDto.UserId&&p.EventId==addEventParticipantDto.EventId);

            if (_participant is not null)
                throw new BusinessException("AlreadyParticipant");

            if (addEventParticipantDto.EventId == 0)
                throw new BusinessException("InvalidEventId");

            if (addEventParticipantDto.UserId == 0)
                throw new BusinessException("InvalidUserId");

            _cacheManager.RemoveByPattern($"EventParticipants.GetListAsync?EventId={addEventParticipantDto.EventId}");

            var eventParticipant = _mapper.Map<EventParticipant>(addEventParticipantDto);

            await _eventParticipantDal.AddAsync(eventParticipant);
        }

        public async Task<IPaginate<EventParticipantDto>> GetParticipantAsync(PageRequest pageRequest, int eventId)
        {
            if (eventId == 0)
                throw new BusinessException("InvalidEventId");

            string cacheKey = $"EventParticipants.GetListAsync?EventId={eventId}&Page={pageRequest.Page}&Size={pageRequest.PageSize}";
            if (_cacheManager.IsAdd(cacheKey))
                return _cacheManager.Get<IPaginate<EventParticipantDto>>(cacheKey);

            var eventParticipants = await _eventParticipantDal.GetListAsync(predicate: e => e.EventId == eventId, size: pageRequest.PageSize, index: pageRequest.Page);

            var result = _mapper.Map<Paginate<EventParticipantDto>>(eventParticipants);

            _cacheManager.Add(cacheKey, result);

            return result;
        }

        public async Task<bool> IsParticipant(int userId,int eventId)
        {
            var _participant = await _eventParticipantDal.GetAsync(p => p.UserId == userId && p.EventId == eventId);

            return _participant is not null;
        }

    }
}
