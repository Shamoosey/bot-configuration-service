using AutoMapper;
using DiscordBot_Backend;
using DiscordBot_Backend.Database;
using DiscordBot_Backend.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IConfigurationService, ConfigurationService>();
builder.Services.AddTransient<IStatusService, StatusService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<ITriggerService, TriggerService>();

var configuration = new ConfigurationBuilder()
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddSingleton(p => new MapperConfiguration(cfg => {
    cfg.AddProfile(new MappingProfile());
}).CreateMapper());

builder.Services.AddDbContext<BotContext>(options =>
    options.UseSqlServer(configuration["ConnectionStrings:DefaultConnection"]
));


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBot",
        builder =>
        {
            builder.WithOrigins(configuration["AngularAppUrl"])
                   .AllowAnyMethod()
                   .AllowAnyHeader();

            builder.WithOrigins(configuration["JoeBotUrl"])
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(x => x.SwaggerEndpoint("/swagger/v1/swagger.json", "Configuration Service v1"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("AllowBot");

app.MapControllers();

string port = configuration["Application:Port"];

app.Run($"http://localhost:{port}");
