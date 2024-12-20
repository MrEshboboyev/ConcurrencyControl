using ConcurrencyControl.Domain.Abstracts.Persistence.Repositories;
using ConcurrencyControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConcurrencyControl.Persistence.Repositories;

public class BankAccountRepository(DemoDbContext context)
    : IBankAccountRepository
{
    private readonly DemoDbContext _context = context;

    public async Task CreateBankAccountAsync(string accountNumber, string ownerName)
    {
        await _context.BankAccounts.AddAsync(new BankAccount()
        {
            AccountNumber = accountNumber,
            OwnerName = ownerName
        });
        
        await _context.SaveChangesAsync();
    }

    public async Task CreateBankAccountAsync(Guid accountId, decimal amount)
    {
        var account = await _context.BankAccounts.FindAsync(accountId)
            ?? throw new ArgumentException("Account not found");
        
        account.Balance += amount;

        try
        {

        }
        catch (DbUpdateConcurrencyException ex)
        {
            await ex.Entries.Single().ReloadAsync();
            Console.WriteLine($"Concurrency exception occured: {ex.Message}");
            throw;
        }
        
    }
}