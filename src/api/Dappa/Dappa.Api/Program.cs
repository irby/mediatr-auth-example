using Dappa.Api;
using Dappa.Api.Behaviors;
using Dappa.Api.Extensions;
using Dappa.Api.Managers;
using Dappa.Core;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<CookieManager>();
builder.Services.AddHealthChecks();

builder.Services.AddLogging()
    .AddMediatR()
    .AddTransient(typeof(IPipelineBehavior<,>), typeof(SecureRequestBehavior<,>))
    .AddRequestValidation()
    .AddDatabase(Guid.NewGuid())
    .AddJwtValidation();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.SeedDatabase();
}

// app.UseHttpsRedirection();
app.MapControllers();
app.UseAuthentication();
app.UseAuthorization();

app.MapHealthChecks("health");

app.Run();
