using ConcurrencyControl.Domain.Entities;

namespace ConcurrencyControl.Domain.Abstracts.Persistence.Repositories;

public interface IBankAccountRepository
{
    Task CreateBankAccountAsync(string accountNumber, string ownerName);
    Task CreateBankAccountAsync(Guid accountId, decimal amount);
}