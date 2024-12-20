using ConcurrencyControl.API.Modules;
using ConcurrencyControl.Domain.Abstracts.Persistence.Repositories;
using ConcurrencyControl.Persistence;
using ConcurrencyControl.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

#region Database connection

builder.Services.AddDbContext<DemoDbContext>(options => 
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection")));

#endregion

#region Repositories lifetimes

builder.Services.AddScoped<IBankAccountRepository, BankAccountRepository>();

#endregion

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.AddBankAccountEndpoints();
app.UseHttpsRedirection();

app.Run();
