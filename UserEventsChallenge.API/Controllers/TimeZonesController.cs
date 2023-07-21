using Microsoft.AspNetCore.Mvc;
using UserEventsChallenge.API.Entities.Dtos;

namespace UserEventsChallenge.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeZonesController : ControllerBase
    {
        [HttpGet]
        public List<TimeZoneInfoDto> GetTimeZoneIds()
        {
            return TimeZoneInfo.GetSystemTimeZones()
                               .Select(t=>
                new TimeZoneInfoDto(t.Id,t.DisplayName))
                               .ToList();
        }
    }
}
