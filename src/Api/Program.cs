using Cinema.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Cinema.Application.Abstractions.IRepositories;
using Cinema.Application.Abstractions;
using Cinema.Application.Services;
using Cinema.Application.Services.IServices;
using Cinema.Infrastructure.Security;
using Cinema.Application.MappingProfiles;
using Cinema.Infrastructure.Repositories;
using Cinema.Infrastructure.Security.Auth;
using Cinema.Domain.Policies;
using Cinema.Domain.Abstractions;
using Cinema.Infrastructure.Time;
using Microsoft.Extensions.Options;
using Cinema.Api;
using Cinema.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseNpgsql(
            builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddControllers()
                .AddJsonOptions(options =>
                        {
                            options.JsonSerializerOptions.Converters
                                .Add(new JsonStringEnumConverter());
                        });
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddAutoMapper(cfg => { }, typeof(UserProfile).Assembly);
builder.Services.AddSingleton<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<ISessionRepository, SessionRepository>();
builder.Services.AddScoped<ISeatRepository, SeatRepository>();
builder.Services.AddScoped<IFilmRepository, FilmRepository>();
builder.Services.AddScoped<IHallRepository, HallRepository>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ISessionService, SessionService>();
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<IFilmService, FilmService>();
builder.Services.AddScoped<IHallService, HallService>();
builder.Services.AddScoped<ISeatService, SeatService>();

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddSingleton<IClock>(_ => new FixedTime(new DateTime(2026, 1, 15, 9, 0, 0)));
}
else
{
    builder.Services.AddSingleton<IClock, SystemClock>();
}

builder.Services.Configure<JwtSettings>(
        builder.Configuration.GetSection("JwtSettings"));

builder.Services.Configure<Cinema.Application.Helpers.CinemaSettings>(
        builder.Configuration.GetSection("CinemaSettings"));

builder.Services.AddSingleton<ICinemaSettings>(sp =>
        sp.GetRequiredService<IOptions<Cinema.Application.Helpers.CinemaSettings>>().Value);

builder.Services.AddSingleton<SessionPolicy>();

builder.Services.AddAuth(builder.Configuration);
var app = builder.Build();

app.UseMiddleware<ExceptionHandlerMiddleware>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
