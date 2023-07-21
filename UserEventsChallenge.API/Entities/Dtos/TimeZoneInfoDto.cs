namespace UserEventsChallenge.API.Entities.Dtos
{
    public class TimeZoneInfoDto
    {
        public string TimeZoneId { get; set; }
        public string TimeZoneName { get; set; }

        public TimeZoneInfoDto()
        {

        }

        public TimeZoneInfoDto(string timeZoneId, string timeZoneName)
        {
            TimeZoneId = timeZoneId;
            TimeZoneName = timeZoneName;
        }
    }
}
