using System;
using NUnit.Framework;
using bankapp_refactored_week4.Class_Libraries;


namespace Test_Projects
{
    public class Tests
    {
        Customer customer = new Customer("Louis", "Otu", "louis@mail.com", "louis");
        Customer customer2 = new Customer("Phil", "Jude", "ph@mail.com", "phil123");

        #region REGISTRATION TESTING SECTION
        /*
         * REGISTER SECTION
         */

        [Test]
        public void CheckIfItIsNotAnAccountType()
        {
            // Arrange

            // Assert
            Assert.Throws<InvalidOperationException>(
                () => Auth.Register(customer, "standard", 4000)
             );
        }

        [Test]
        public void CheckIfItIsNotSavingsMinimumOpeningAccount()
        {
            // Arrange
            var customerUser = new Customer("Blessed", "Jacob", "bjac@mail.com", "jac345");

            // Assert
            Assert.Throws<InvalidOperationException>(
                () => Auth.Register(customerUser, "savings", 50)
             );
        }

        [Test]
        public void CheckIfItIsNotCurrentMinimumOpeningAccount()
        {
            // Arrange
            var customerUser = new Customer("Blessed", "Jacob", "bjac@mail.com", "jac345");

            // Assert
            Assert.Throws<InvalidOperationException>(
                () => Auth.Register(customerUser, "current", 700)
             );
        }

        [Test]
        public void CheckIfSavingsAccountOpens()
        {
            // Arrange
            Account account = Auth.Register(customer, "savings", 500);

            // Act
            var check = account.Balance;

            // Assert
            Assert.That(check, Is.EqualTo(500));
        }

        [Test]
        public void CheckIfCurrentAccountOpens()
        {
            // Arrange
            Account account = Auth.Register(customer, "current", 4500);

            // Act
            var check = account.Balance;

            // Assert
            Assert.That(check, Is.EqualTo(4500));
        }

        #endregion

        #region LOGIN SECTION TESTING
        /*
         * LOGIN SECTION
         */
        [Test]
        public void LoginFail()
        {
            // Arrange
            Customer loginUser = new Customer("James", "Akin", "james@mail.com", "pass123");

            // Assert
            Assert.Throws<NullReferenceException>(
                () => Auth.Login("james@mail.com", "james1234")
             );

        }

        [Test]
        public void LoginPass()
        {
            // Arrange
            Customer loginUser = new Customer("James", "Akin", "james@mail.com", "pass123");
            Auth.Login(loginUser.Email, loginUser.Password);

            // Act
            var check = Auth.GetAllLoggedInUser(loginUser.Email, loginUser.Password);

            // Assert
            Assert.That(check.Email, Is.EqualTo("james@mail.com"));
        }
        #endregion

        #region DEPOSIT TESTING SECTION
        /*
         * DEPOSIT SECTION
         */
        [Test]
        public void CheckAccountDepositIfItIsNegative()
        {
            // Arrange
            Account account = new Account(customer, "savings", 4000);

            // Assert
            Assert.Throws<InvalidOperationException>(
                () => account.MakeDeposit(account, "savings", -1, DateTime.Now, "First Deposit")
             );
        }

        [Test]
        public void CheckAccountDepositedIsAdded()
        {
            // Arrange
            Account account = new Account(customer, "savings", 4000);

            // Act
            account.MakeDeposit(account, "savings", 200, DateTime.Now, "First Deposit");

            // Assert
            Assert.That(account.Balance, Is.EqualTo(4200));
        }
        #endregion

        #region WITHDRAWAL SECTION TESTING
        /*
         * WITHDRAWAL SECTION
         */

        [Test]
        public void CheckAccountWithdrawIfItIsNegative()
        {
            // Arrange
            var customer = new Customer("Louis", "Otu", "louis@mail.com", "louis");

            // Act
            var account = new Account(customer, "savings", 4000);

            // Assert
            Assert.Throws<InvalidOperationException>(
                () => account.MakeWithdrawal(account, "savings", -1, DateTime.Now, "First Withdrawal")
             ); ;
        }

        [Test]
        public void CheckAccountisWithdrawFromSavingsFail()
        {
            // Arrange
            var customer = new Customer("Louis", "Otu", "louis@mail.com", "louis");
            var account = new Account(customer, "savings", 4000);

            // Assert
            Assert.Throws<InvalidOperationException>(
                () => account.MakeWithdrawal(account, "savings", 3950, DateTime.Now, "First Withdrawal")
            );
        }

        [Test]
        public void CheckAccountisWithdrawFrom()
        {
            // Arrange
            var customer = new Customer("Louis", "Otu", "louis@mail.com", "louis");
            var account = new Account(customer, "savings", 4000);

            // Act
            account.MakeWithdrawal(account, "savings", 200, DateTime.Now, "First Withdrawal");

            // Assert

            Assert.That(account.Balance, Is.EqualTo(3800));
        }

        #endregion

        #region TRANFER SECTION TESTING IMPLEMENTATION
        /*
         * TRANFER SECTION
         */

        [Test]
        public void CheckAccountTransferIfItIsToSameAccountNumber()
        {
            // Arrange
            Account account = new Account(customer, "savings", 4000);

            // Assert
            Assert.Throws<InvalidOperationException>(
                () => account.Transfer(account, "current", 1250, DateTime.Now, "Transfer of Funds")
             );
        }

        [Test]
        public void CheckTransfersFromSender()
        {
            // Arrange
            Account account = new Account(customer, "savings", 4000);
            Account account2 = new Account(customer2, "savings", 4000);

            // Act
            account.Transfer(account2, "savings", 2000, DateTime.Now, "Transfer");

            // Assert
            Assert.That(account.Balance, Is.EqualTo(2000));
        }

        [Test]
        public void CheckTransfersToReceiver()
        {
            // Arrange
            Account account = new Account(customer, "savings", 4000);
            Account account2 = new Account(customer2, "savings", 4000);

            // Act
            account.Transfer(account2, "savings", 2000, DateTime.Now, "Transfer");

            // Assert
            Assert.That(account2.Balance, Is.EqualTo(6000));
        }
        #endregion

        #region TRANSACTION TESTING SECTION
        /*
         * TRANSACTION SECTION
         */
        [Test]
        public void CheckTransactionCount()
        {
            // Arrange
            Account accountCur = Auth.Register(customer, "current", 4500);
            Account accountSav = Auth.Register(customer, "savings", 1250);
            accountSav.MakeWithdrawal(accountCur, "current", 200, DateTime.Now, "Taking out funds");
            accountCur.Transfer(accountSav, "savings", 1500, DateTime.Now, "Transfer");

            Console.WriteLine(BankDB.AllTransactions.Count);

            // Act
            var transactionNow = BankDB.AllTransactions.Count;

            // Assert
            Assert.That(transactionNow, Is.EqualTo(15));
        }

        #endregion

    }
}