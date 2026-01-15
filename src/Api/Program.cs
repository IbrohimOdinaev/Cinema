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

builder.Services.AddAutoMapper(cfg => { }, typeof(UserProfile).Assembly);
builder.Services.AddSingleton<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

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

builder.Services.Configure<JwtSettings>(
        builder.Configuration.GetSection("JwtSettings"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
