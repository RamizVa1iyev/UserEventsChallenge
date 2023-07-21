using Microsoft.EntityFrameworkCore;
using UserEventsChallenge.API.DataAccess.Concrete.EntityFramework.Configurations;
using UserEventsChallenge.API.Entities.Concrete;

namespace UserEventsChallenge.API.DataAccess.Abstract.EntityFramework.Contexts;

public class UserEventsDbContext:DbContext
{
    protected IConfiguration Configuration { get; set; }

    public DbSet<Event> Events { get; set; }
    public DbSet<EventParticipant> EventParticipants { get; set; }
    public DbSet<EventInvite> EventInvites { get; set; }

    public UserEventsDbContext(DbContextOptions dbContextOptions, IConfiguration configuration) : base(dbContextOptions)
    {
        Configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
            base.OnConfiguring(
                optionsBuilder.UseMySQL(Configuration.GetConnectionString("UserEventsConnectionString")));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new EventConfigurtaion());
        modelBuilder.ApplyConfiguration(new EventParticipantConfigurtaion());
        modelBuilder.ApplyConfiguration(new EventInviteConfigurtaion());
    }
}
