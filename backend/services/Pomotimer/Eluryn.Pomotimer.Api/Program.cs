using System.Net;
using Eluryn.Pomotimer.Api.Data;
using Eluryn.Pomotimer.Api.Repositories;
using Eluryn.Pomotimer.Api.Services;
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

    var trustedProxy = builder.Configuration["ReverseProxy:TrustedProxy"];
    if (!string.IsNullOrWhiteSpace(trustedProxy))
    {
        options.KnownProxies.Add(IPAddress.Parse(trustedProxy));
    }
});

// OpenAPI
builder.Services.AddOpenApi();
builder.Services.AddControllers();

builder.Services.AddDbContext<PomotimerDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Db")));

builder.Services.AddScoped<IPomodoroSettingsRepository, PomodoroSettingsRepository>();
builder.Services.AddScoped<IPomodoroSettingsService, PomodoroSettingsService>();

var app = builder.Build();

app.UseForwardedHeaders();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapGet("/health", () => Results.Ok(new { status = "ok" }));
app.MapControllers();

app.Run();