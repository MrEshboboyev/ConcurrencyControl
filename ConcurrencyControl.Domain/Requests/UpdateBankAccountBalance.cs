namespace ConcurrencyControl.Domain.Requests;

public record UpdateBankAccountBalance(Guid AccountId, decimal Balance);