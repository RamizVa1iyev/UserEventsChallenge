using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserEventsChallenge.API.Entities.Concrete;

namespace UserEventsChallenge.API.DataAccess.Concrete.EntityFramework.Configurations;

public class EventConfigurtaion : IEntityTypeConfiguration<Event>
{
    public void Configure(EntityTypeBuilder<Event> builder)
    {
        builder.ToTable("Events");

        builder.HasKey(e => e.Id);

        builder.HasMany(e => e.EventParticipants).WithOne(e => e.Event);
    }
}
