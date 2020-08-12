using System;
using System.Collections.Generic;
using System.Text;

namespace bankapp_refactored_week4.Class_Libraries
{
    public class BankDB
    {
        // Databases Storage for my Bank Application
        public static List<Customer> AllCustomer { get; set; } = new List<Customer>();
        public static List<Customer> LoggedUser { get; set; } = new List<Customer>();
        public static List<Account> AllAccounts { get; set; } = new List<Account>();
        public static List<Transaction> AllTransactions { get; set; } = new List<Transaction>();
    }
}
