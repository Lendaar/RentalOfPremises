using Microsoft.EntityFrameworkCore;
using RentalOfPremises.Api.Infrastructure;
using RentalOfPremises.Context;
using RentalOfPremises.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(x =>
{
    x.Filters.Add<RentalOfPremisesExceptionFilter>();
})
    .AddControllersAsServices();

builder.Services.AddEndpointsApiExplorer();
builder.Services.GetSwaggerGen();
builder.Services.AddDependences();

var conString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContextFactory<RentalOfPremisesContext>(options => options.UseSqlServer(conString), ServiceLifetime.Scoped);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.GetSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
