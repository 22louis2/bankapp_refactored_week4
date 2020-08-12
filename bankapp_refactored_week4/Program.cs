using System;
using System.Threading;
using bankapp_refactored_week4.Class_Libraries;

namespace bankapp_refactored_week4
{
    class Program
    {
        static void Main(string[] args)
        {
            var customer = new Customer("Louis", "Otu", "@mail.com", "pass123");
            var customer1 = new Customer("Jude", "Phils", "ph@mail.com", "pass123");
            Console.WriteLine(customer.Email);

            var account = Auth.Register(customer, "savings", 4000);
            var account1 = Auth.Register(customer1, "savings", 10000);

            account1.Transfer(account, "savings", 4000, DateTime.Now, "Transfer");

            Transaction.GetAllTransactions();          

            Console.WriteLine(account.Balance);

            // Checking if a Customer tries to transfer to between same account number
            // account.Transfer(account, "savings", 4000, DateTime.Now, "Transfer");
        }
    }
}
