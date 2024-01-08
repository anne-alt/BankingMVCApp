using System;

public class Transaction
{
    public int Id { get; set; }
    public int BankAccountId { get; set; }
    public DateTime TransactionDate { get; set; }
    public string Description { get; set; } = default!;

    // Navigation property for the associated bank account
    public BankAccount BankAccount { get; set; } = default!;
}
