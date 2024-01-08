public class BankAccount 
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
    public int Balance { get; private set; } = default!;
    public List<string> TransactionHistory { get; private set; } = default!;

    public BankAccount(string name, string email)
    {
        Name = name;
        Email = email;
        TransactionHistory = new List<string>();
    }

    public void Deposit(int amount) {
        Balance += amount;
        TransactionHistory.Add($"Deposited: {amount}");
    }

    public void Withdrawal(int amount) {
    
        if (amount > Balance)
        {
            // Handle insufficient balance
            TransactionHistory.Add($"Withdrawal failed. Insufficient balance.");
        }
        else
        {
            Balance -= amount;
            TransactionHistory.Add($"Withdrawn: {amount}");
        }
    }




}