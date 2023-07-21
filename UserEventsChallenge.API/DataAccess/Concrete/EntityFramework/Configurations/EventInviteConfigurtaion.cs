using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using UserEventsChallenge.API.Entities.Concrete;

namespace UserEventsChallenge.API.DataAccess.Concrete.EntityFramework.Configurations;

public class EventInviteConfigurtaion : IEntityTypeConfiguration<EventInvite>
{
    public void Configure(EntityTypeBuilder<EventInvite> builder)
    {
        builder.ToTable("EventInvites");

        builder.HasKey(e => e.Id);

        builder.HasOne(e => e.Event).WithMany(e => e.EventInvites);
    }
}
