using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserEventsChallenge.API.Entities.Concrete;

namespace UserEventsChallenge.API.DataAccess.Concrete.EntityFramework.Configurations;

public class EventParticipantConfigurtaion : IEntityTypeConfiguration<EventParticipant>
{
    public void Configure(EntityTypeBuilder<EventParticipant> builder)
    {
        builder.ToTable("EventParticipants");

        builder.HasKey(e=>e.Id);
        builder.HasOne(e => e.Event).WithMany(e => e.EventParticipants);

    }
}
