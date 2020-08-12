using System;
using System.Collections.Generic;
using System.Text;

namespace bankapp_refactored_week4.Class_Libraries
{
    public class Customer
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int Id { get; set; }
        public string Password { get; set; }

        // initializing my customer id from 1;
        private static int id = 1;
        public Customer(string firstName, string lastName, string email, string password)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
            this.Id = id;
            this.Password = password;
            id++;

            // Adding every this on this constructor to my Customer List storage
            BankDB.AllCustomer.Add(this);
        }
    }
}
