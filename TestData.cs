using Faker;
using System;
using System.Collections.Generic;

public static class FakerBankAccounts
{
    public static List<BankAccount> GenerateFakeBankAccounts(int count)
    {
        var bankAccounts = new List<BankAccount>();

        for (int i = 0; i < count; i++)
        {
            var account = new BankAccount(Faker.Name.FullName(), Faker.Internet.Email())
            {
                Name = Faker.Name.FullName(),
                Email = Faker.Internet.Email()
            };

            // Generate random transactions (deposits and withdrawals)
            for (int j = 0; j < Faker.RandomNumber.Next(1, 10); j++)
            {
                int amount = Faker.RandomNumber.Next(10, 500);
                if (Faker.RandomNumber.Next(0, 2) == 0)
                {
                    account.Deposit(amount);
                }
                else
                {
                    account.Withdrawal(amount);
                }
            }

            bankAccounts.Add(account);
        }

        return bankAccounts;
    }
}