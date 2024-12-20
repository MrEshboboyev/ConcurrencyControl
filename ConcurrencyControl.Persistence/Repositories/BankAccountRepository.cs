using ConcurrencyControl.Domain.Abstracts.Persistence.Repositories;
using ConcurrencyControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Polly;
using Polly.Retry;

namespace ConcurrencyControl.Persistence.Repositories;

public class BankAccountRepository(DemoDbContext context)
    : IBankAccountRepository
{
    private readonly DemoDbContext _context = context;
    private readonly AsyncRetryPolicy _retryPolicy = Policy.Handle<DbUpdateConcurrencyException>()
        .WaitAndRetryAsync(3, retryAttempt => 
            TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

    public async Task CreateBankAccountAsync(string accountNumber, string ownerName)
    {
        var bankAccount = new BankAccount
        {
            AccountNumber = accountNumber,
            OwnerName = ownerName,
            RowVersion = Guid.NewGuid().ToByteArray() // Temporary fix
        };

        await _context.BankAccounts.AddAsync(bankAccount);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateBalanceAsync(Guid accountId, decimal amount)
    {
        await _retryPolicy.ExecuteAsync(async () =>
        {
            var account = await _context.BankAccounts.FindAsync(accountId)
                          ?? throw new ArgumentException("Account not found");
        
            account.Balance += amount;

            try
            {
                _context.BankAccounts.Update(account);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                await ex.Entries.Single().ReloadAsync();
                Console.WriteLine($"Concurrency exception occured: {ex.Message}");
                throw;
            }
        });
    }
}