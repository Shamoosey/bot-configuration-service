using AutoMapper;
using Joebot_Backend;
using Joebot_Backend.Database;
using Joebot_Backend.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IConfigurationService, ConfigurationService>();
builder.Services.AddTransient<IStatusService, StatusService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<ITriggerService, TriggerService>();

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddSingleton<IMapper>(p => new MapperConfiguration(cfg => {
    cfg.AddProfile(new MappingProfile());
}).CreateMapper());

builder.Services.AddDbContext<JoeContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")
));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
