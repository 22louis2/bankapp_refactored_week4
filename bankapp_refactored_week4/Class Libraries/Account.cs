using System;
using System.Collections.Generic;
using System.Text;

namespace bankapp_refactored_week4.Class_Libraries
{
    public class Account
    {
        public string AccountNumber { get; set; }
        public static int accountNumberSeed = 1234567890;
        public decimal Amount { get; set; }
        public string Type { get; set; }
        public string Note { get; set; }
        public string Owner { get; set; }
        public int OwnerId { get; set; }

        public decimal Balance { get; set; }

        public Account(Customer customer, string type, decimal initialBalance)
        {
            // Check if the type inputted, doesn't validate type
            if (type != "savings" && type != "current")
            {
                throw new InvalidOperationException("Account must be Savings or Current");
            }
            // Check if the type inputted and first deposit doesn't validate requirement for savings account
            if (initialBalance < 100 && type == "savings")
            {
                throw new InvalidOperationException("Not sufficient funds to open a savings account");
            }
            // Check if the type inputted and first deposit doesn't validate current account requirement
            if (initialBalance < 1000 && type == "current")
            {
                throw new InvalidOperationException("Not sufficient funds to open a current account");
            }

            this.Type = type;
            this.OwnerId = customer.Id;
            this.Owner = $"{customer.FirstName} {customer.LastName}";
            this.AccountNumber = accountNumberSeed.ToString();
            accountNumberSeed++;

            //Call makedeposit method if it passes all requirements
            MakeDeposit(this, type, initialBalance, DateTime.Now, "Initial Balance");

            // Add to List storage of accounts
            BankDB.AllAccounts.Add(this);
        }

        public void MakeDeposit(Account customer, string type, decimal amount, DateTime date, string note)
        {
            // Check amount inputted if is less than or equal to zero
            if (amount <= 0)
            {
                throw new InvalidOperationException("Amount to deposit must be positive");
            }
            //Add to my balance properties and call my transaction method and pass to my Transaction List
            Balance += amount;
            new Transaction(customer.OwnerId, type, Balance, date, note);
        }

        public void MakeWithdrawal(Account customer, string type, decimal amount, DateTime date, string note)
        {
            // Check amount inputted if is less than or equal to zero
            if (amount <= 0)
            {
                throw new InvalidOperationException("Amount to deposit must be positive");
            }
            // Check amount inputted for savings type of account if withdrawn is less than its limit
            if (Balance - amount < 100 && Type == "savings")
            {
                throw new InvalidOperationException("Not sufficient funds for this withdrawal");
            }

            // Add to my account and populate my Transaction list
            Balance -= amount;
            new Transaction(customer.OwnerId, type, Balance, date, note);
        }

        public void Transfer(Account recipient, string type, decimal amount, DateTime date, string note)
        {
            // Looping through my Account List
            foreach (var acc in BankDB.AllAccounts)
            {
                // If account number inputted is the same as the account number to be transferred to, throw error
                if (AccountNumber.Equals(recipient.AccountNumber))
                {
                    throw new InvalidOperationException("No transfer possible between same account");
                }

                // if the Recipient customer account number exists, it should make a transfer
                if (acc.AccountNumber.Equals(recipient.AccountNumber))
                {
                    MakeWithdrawal(this, type, amount, DateTime.Now, note);
                    recipient.MakeDeposit(recipient, type, amount, DateTime.Now, note);

                    Console.WriteLine($"Your transfer to {recipient.Owner} is successful \n" +
                        $"and your balance is {this.Balance}");
                    break;

                }
            }
        }
    }
}
