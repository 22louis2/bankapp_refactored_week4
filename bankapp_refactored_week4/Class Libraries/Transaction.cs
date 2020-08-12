using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace bankapp_refactored_week4.Class_Libraries
{
    public class Transaction
    {
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Notes { get; set; }
        public string Type { get; set; }
        public Account Account { get; set; }
        public Customer Customer { get; set; }
        public int AccID { get; set; }
        public decimal Balance { get; }

        public Transaction(int accID, string type, decimal balance, DateTime date, string note)
        {
            this.Balance = balance;
            this.Date = date;
            this.Notes = note;
            this.AccID = accID;
            this.Type = type;

            // Add every transaction to my Transactions database
            BankDB.AllTransactions.Add(this);
        }

        public static void GetAllTransactions()
        {
            // Looping through my Transaction Database and returning the transaction list
            foreach (var item in BankDB.AllTransactions)
            {
                Console.WriteLine($"{item.AccID} {item.Balance} {item.Type} {item.Date}");
            }
        }

    }
}
