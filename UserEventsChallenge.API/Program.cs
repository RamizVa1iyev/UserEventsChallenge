using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Reflection;
using UserEventsChallenge.API.Business.Abstract;
using UserEventsChallenge.API.Business.Caching;
using UserEventsChallenge.API.Business.Caching.Microsoft;
using UserEventsChallenge.API.Business.Concrete;
using UserEventsChallenge.API.DataAccess.Abstract;
using UserEventsChallenge.API.DataAccess.Abstract.EntityFramework;
using UserEventsChallenge.API.DataAccess.Abstract.EntityFramework.Contexts;
using UserEventsChallenge.API.DataAccess.Concrete;
using UserEventsChallenge.API.DataAccess.Concrete.EntityFramework;
using UserEventsChallenge.API.MiddleWares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<UserEventsDbContext>
    (
       opt => opt.UseMySQL(builder.Configuration.GetConnectionString("UserEventsConnectionString"))
    );

builder.Services.AddScoped<IEventDal, EfEventRepository>();
builder.Services.AddScoped<IEventManager, EventManager>();

builder.Services.AddScoped<IEventParticipantDal, EfEventParticipantRepository>();
builder.Services.AddScoped<IEventParticipantManager, EventParticipantManager>();

builder.Services.AddScoped<IEventInviteDal, EfEventInviteRepository>();
builder.Services.AddScoped<IEventInviteManager, EventInviteManager>();

builder.Services.AddSingleton<ICacheManager, MemoryCacheManager>();

builder.Services.AddMemoryCache();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<UserEventsDbContext>();
    db.Database.Migrate();
}

app.ConfigureCustomExceptionMiddleware();

app.MapControllers();

app.Run();
