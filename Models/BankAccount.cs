public class BankAccount(string name, string email)
{
    public int Id { get; set; }
    public string Name { get; set; } = name;
    public string Email { get; set; } = email;
    public int Balance { get; private set; } = default!;
    public List<Transaction> Transactions { get; private set; } = [];

    public void Deposit(int amount) 
    {
        Balance += amount;
        RecordTransaction($"Deposited: {amount}");
    }

    public void Withdrawal(int amount) 
    {
        if (amount > Balance)
        {
            RecordTransaction($"Withdrawal failed. Insufficient balance.");
        }
        else
        {
            Balance -= amount;
            RecordTransaction($"Withdrawn: {amount}");
        }
    }

    private void RecordTransaction(string description)
    {
        var transaction = new Transaction
        {
            BankAccountId = Id,
            TransactionDate = DateTime.Now,
            Description = description
        };
        Transactions.Add(transaction);
    }

    public void UpdateEmail(string newEmail)
    {
        Email = newEmail;
        RecordTransaction($"Email updated to: {newEmail}");
    }
    public void MoveBalanceToMobile()
    {
        // Simulate sending the balance to the mobile number associated with the account
        RecordTransaction($"Balance moved to mobile");
        Balance = 0; // Set balance to zero after moving it
    }

}