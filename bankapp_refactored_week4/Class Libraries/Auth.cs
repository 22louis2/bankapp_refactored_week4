using System;
using System.Collections.Generic;
using System.Text;

namespace bankapp_refactored_week4.Class_Libraries
{
    public class Auth
    {
        #region AUTHENTICATING MY CUSTOMER
        // Registering a customer using a callback method to call my Account constructor
        public static Account Register(Customer customer, string type, decimal initialBalance) =>
                new Account(customer, type, initialBalance);

        // Login implementation code up
        public static void Login(string email, string password)
        {
            // looping through my List storage to get the customer's details for login
            var check = BankDB.AllCustomer.Find(x => x.Email == email && x.Password == password);

            /* 
             * checking if the .Find method returns a List and if it does, should check if the email and 
             * password matches any data in the list
             */
            if (check.Email.Equals(email) && check.Password.Equals(password))
            {
                BankDB.LoggedUser.Add(check);
            }
            /*
             * If it returns null or fails while validating, if both the email and password, 
             * does not match, it should throw an error 
            */
            else
            {
                throw new NullReferenceException("Wrong Email or Password");
            }
        }

        // Method to check all the logged in user available in the database
        public static Customer GetAllLoggedInUser(string email, string password)
        {
            // Should return all the logged in user(s) available in the List
            var check = BankDB.LoggedUser.Find(x => x.Email == email && x.Password == password);

            // returns null or the number of List, if found 
            return check;
        }

        #endregion
    }
}
