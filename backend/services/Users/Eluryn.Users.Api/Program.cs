using System.Net;
using Eluryn.Users.Api.Data;
using Eluryn.Users.Api.Repositories;
using Eluryn.Users.Api.Services;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders =
        ForwardedHeaders.XForwardedFor |
        ForwardedHeaders.XForwardedProto |
        ForwardedHeaders.XForwardedHost;

    options.ForwardLimit = 1;
    options.KnownProxies.Add(IPAddress.Parse("172.21.0.10"));
});

builder.Services.AddOpenApi();
builder.Services.AddControllers();

builder.Services.AddDbContext<UsersDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Db")));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

app.Logger.LogInformation("===== PROGRAM RELOADED 123 =====");

app.Logger.LogInformation(
    "Users API starting up. Env={Environment}",
    app.Environment.EnvironmentName);

var conn = builder.Configuration.GetConnectionString("Db");
app.Logger.LogInformation("DB configured: {HasConnectionString}", !string.IsNullOrWhiteSpace(conn));

app.UseForwardedHeaders();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapGet("/health", () => Results.Ok(new { status = "ok" }));

app.MapControllers();

app.Run();