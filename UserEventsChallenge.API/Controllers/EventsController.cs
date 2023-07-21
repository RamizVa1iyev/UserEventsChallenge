using Microsoft.AspNetCore.Mvc;
using UserEventsChallenge.API.Business.Abstract;
using UserEventsChallenge.API.Entities.Dtos;
using UserEventsChallenge.API.Entities.Paging;

namespace UserEventsChallenge.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IEventManager _eventManager;
        public EventsController(IEventManager eventManager)
        {
            _eventManager = eventManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserEvents([FromQuery]PageRequest pageRequest,int userId)
        {
            var result = await _eventManager.GetListAsync(pageRequest,userId);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEventById(int id)
        {
            var result = await _eventManager.GetAsync(id);

            return Ok(result);
        }

        [HttpPost]

        public async Task<IActionResult> AddEvent(AddEventDto addEventDto)
        {
            await _eventManager.AddAsync(addEventDto);

            return Ok();
        }


        [HttpPut]

        public async Task<IActionResult> UpdateEvent(UpdateEventDto updateEventDto)
        {
            await _eventManager.UpdateAsync(updateEventDto);

            return Ok();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            await _eventManager.DeleteAsync(id);

            return Ok();
        }
    }
}
