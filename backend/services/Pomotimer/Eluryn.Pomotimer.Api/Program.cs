using Microsoft.AspNetCore.HttpOverrides;
using System.Net;
using Microsoft.EntityFrameworkCore;
using Eluryn.Pomotimer.Api.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders =
        ForwardedHeaders.XForwardedFor |
        ForwardedHeaders.XForwardedProto |
        ForwardedHeaders.XForwardedHost;

    options.ForwardLimit = 1;

    // Trust Traefik IP
    options.KnownProxies.Add(IPAddress.Parse("172.21.0.10"));
});

// OpenAPI
builder.Services.AddOpenApi();

// Controllers
builder.Services.AddControllers();

// EF Core
builder.Services.AddDbContext<PomotimerDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Db")));


var app = builder.Build();

app.UseForwardedHeaders();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapGet("/health", () => Results.Ok(new { status = "ok" }));

app.MapControllers();

app.Run();
