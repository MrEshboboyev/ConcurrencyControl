using ConcurrencyControl.Domain.Abstracts.Persistence.Repositories;
using ConcurrencyControl.Domain.Requests;
using Microsoft.AspNetCore.Mvc;

namespace ConcurrencyControl.API.Modules;

public static class BankAccountModule
{
    public static void AddBankAccountEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPatch("/api/bank-account/balance",
            async ([FromBody] UpdateBankAccountBalance request,
            IBankAccountRepository repository) =>
        {
            await repository.UpdateBalanceAsync(request.AccountId, request.Balance);
            return Results.Ok(); 
        });
        
        app.MapPost("/api/bank-account",
            async ([FromBody] CreateBankAccount request,
                IBankAccountRepository repository) =>
            {
                await repository.CreateBankAccountAsync(request.AccountNumber, request.OwnerName);
                return Results.Ok(); 
            });
    }
}