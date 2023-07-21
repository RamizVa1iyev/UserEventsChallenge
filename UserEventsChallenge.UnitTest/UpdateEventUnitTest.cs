using AutoMapper;
using Moq;
using UserEventsChallenge.API.Business.Abstract;
using UserEventsChallenge.API.Business.Caching;
using UserEventsChallenge.API.Business.Concrete;
using UserEventsChallenge.API.DataAccess.Concrete;
using UserEventsChallenge.API.Entities.Dtos;
using UserEventsChallenge.API.Entities.Exceptions;

namespace UserEventsChallenge.UnitTest
{
    public class Tests
    {
        private IEventManager _eventManager;
        [SetUp]
        public void Setup()
        {
            var mockEventDal = new Mock<IEventDal>();
            var mockCacheManager = new Mock<ICacheManager>();
            var mockMapper = new Mock<IMapper>();
            _eventManager = new EventManager(mockEventDal.Object, mockCacheManager.Object, mockMapper.Object);
        }

        [Test]
        public void Add_ZeroId_ThrowBusinessException()
        {
            var updateEventDto = new UpdateEventDto() { Id = 0 };

            Task actual() => _eventManager.UpdateAsync(updateEventDto);

            var exception = Assert.ThrowsAsync<BusinessException>(actual);

            Assert.That(exception.Message, Is.EqualTo("InvalidId"));
        }
    }
}