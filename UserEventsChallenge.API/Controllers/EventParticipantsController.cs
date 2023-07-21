using Microsoft.AspNetCore.Mvc;
using UserEventsChallenge.API.Business.Abstract;
using UserEventsChallenge.API.Entities.Dtos;
using UserEventsChallenge.API.Entities.Paging;

namespace UserEventsChallenge.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventParticipantsController : ControllerBase
    {
        private readonly IEventParticipantManager _eventParticipantManager;

        public EventParticipantsController(IEventParticipantManager eventParticipantManager)
        {
            _eventParticipantManager = eventParticipantManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetEventParticipants([FromQuery] PageRequest pageRequest,int eventId)
        {
            var result = await _eventParticipantManager.GetParticipantAsync(pageRequest,eventId);

            return Ok(result);
        }

        [HttpGet("IsParticipant")]
        public async Task<IActionResult> IsUserEventParticipant(int userId,int eventId)
        {
            var result = await _eventParticipantManager.IsParticipant(userId, eventId);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddEventParticipant(AddEventParticipantDto participant)
        {
            await _eventParticipantManager.AddParticipantAsync(participant);

            return Ok();
        }
    }
}
