using Microsoft.AspNetCore.HttpOverrides;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders =
        ForwardedHeaders.XForwardedFor | // forward client IP
        ForwardedHeaders.XForwardedProto | // forward original scheme (https)
        ForwardedHeaders.XForwardedHost; // forward original host

    options.ForwardLimit = 1;

    // Trust Traefik IP
    // options.KnownProxies.Add(IPAddress.Parse("172.20.0.10"));
    options.KnownProxies.Add(IPAddress.Parse("172.21.0.10"));
});

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

app.UseForwardedHeaders();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", (HttpContext ctx) =>
{
    var xff = ctx.Request.Headers["X-Forwarded-For"].ToString();
    var xreal = ctx.Request.Headers["X-Real-Ip"].ToString();
    var xorigFor = ctx.Request.Headers["X-Original-For"].ToString();
    var forwarded = ctx.Request.Headers["Forwarded"].ToString();

    app.Logger.LogInformation("RemoteIp={RemoteIp} Scheme={Scheme}", 
        ctx.Connection.RemoteIpAddress?.ToString(), ctx.Request.Scheme);

    app.Logger.LogInformation("XFF='{XFF}' X-Real-Ip='{XRealIp}' X-Original-For='{XOrigFor}' Forwarded='{Forwarded}'",
        xff, xreal, xorigFor, forwarded);

    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
