// File: App/MenuManager.cs
using System;
using PaymentSystemApp.Core.Entities;
using PaymentSystemApp.Core.Interfaces;

namespace PaymentSystemApp.App
{
    public class MenuManager
    {
        private readonly IUserService _userService;
        private readonly IPaymentService _paymentService;
        private User _currentUser;

        public MenuManager(IUserService userService, IPaymentService paymentService)
        {
            _userService = userService;
            _paymentService = paymentService;
        }

        public void Start()
        {
            while (true)
            {
                Console.WriteLine("\n1. Register\n2. Login\n3. Exit");
                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1": Register(); break;
                    case "2": Login(); break;
                    case "3": return;
                    default: Console.WriteLine("Invalid option."); break;
                }
            }
        }

        private void Register()
        {
            Console.Write("Username: ");
            var username = Console.ReadLine();
            Console.Write("Password: ");
            var password = Console.ReadLine();

            if (_userService.Register(username, password))
                Console.WriteLine("User registered successfully.");
            else
                Console.WriteLine("Username already exists.");
        }

        private void Login()
        {
            Console.Write("Username: ");
            var username = Console.ReadLine();
            Console.Write("Password: ");
            var password = Console.ReadLine();

            _currentUser = _userService.Login(username, password);
            if (_currentUser == null)
            {
                Console.WriteLine("Login failed.");
                return;
            }

            Console.WriteLine($"Welcome {_currentUser.Username}!");
            if (_currentUser.IsAdmin) AdminMenu();
            else UserMenu();
        }

        private void UserMenu()
        {
            while (true)
            {
                Console.WriteLine("\n1. View Balance\n2. Send Payment\n3. View Transactions\n4. Logout");
                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1": Console.WriteLine($"Balance: ${_currentUser.Balance}"); break;
                    case "2": SendPayment(); break;
                    case "3": ViewUserTransactions(); break;
                    case "4": _currentUser = null; return;
                    default: Console.WriteLine("Invalid option."); break;
                }
            }
        }

        private void SendPayment()
        {
            Console.Write("Recipient: ");
            var recipient = Console.ReadLine();
            Console.Write("Amount: ");
            var amountStr = Console.ReadLine();

            if (!decimal.TryParse(amountStr, out var amount) || amount <= 0)
            {
                Console.WriteLine("Invalid amount.");
                return;
            }

            if (_paymentService.Send(_currentUser, recipient, amount))
                Console.WriteLine("Payment sent successfully.");
            else
                Console.WriteLine("Transaction failed.");
        }

        private void ViewUserTransactions()
        {
            var txs = _paymentService.GetUserTransactions(_currentUser.Username);
            foreach (var tx in txs)
            {
                Console.WriteLine($"{tx.Timestamp}: {tx.Type} ${tx.Amount} from {tx.FromUser} to {tx.ToUser}");
            }
        }

        private void AdminMenu()
        {
            while (true)
            {
                Console.WriteLine("\nAdmin Panel:");
                Console.WriteLine("1. View All Users\n2. View All Transactions\n3. Promote User\n4. Demote User\n5. Delete User\n6. View User Transactions\n7. Logout");
                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1": ViewAllUsers(); break;
                    case "2": ViewAllTransactions(); break;
                    case "3": PromoteUser(); break;
                    case "4": DemoteUser(); break;
                    case "5": DeleteUser(); break;
                    case "6": ViewSpecificUserTransactions(); break;
                    case "7": _currentUser = null; return;
                    default: Console.WriteLine("Invalid option."); break;
                }
            }
        }

        private void ViewAllUsers()
        {
            foreach (var user in _userService.GetAllUsers())
            {
                Console.WriteLine($"Username: {user.Username}, Balance: ${user.Balance}, Admin: {user.IsAdmin}");
            }
        }

        private void ViewAllTransactions()
        {
            foreach (var tx in _paymentService.GetAllTransactions())
            {
                Console.WriteLine($"{tx.Timestamp}: {tx.Type} ${tx.Amount} from {tx.FromUser} to {tx.ToUser}");
            }
        }

        private void PromoteUser()
        {
            Console.Write("Username to promote: ");
            var username = Console.ReadLine();
            _userService.PromoteToAdmin(username);
            Console.WriteLine("User promoted.");
        }

        private void DemoteUser()
        {
            Console.Write("Username to demote: ");
            var username = Console.ReadLine();
            _userService.DemoteUser(username);
            Console.WriteLine("User demoted.");
        }

        private void DeleteUser()
        {
            Console.Write("Username to delete: ");
            var username = Console.ReadLine();
            _userService.DeleteUser(username);
            Console.WriteLine("User deleted.");
        }

        private void ViewSpecificUserTransactions()
        {
            Console.Write("Username to view transactions: ");
            var username = Console.ReadLine();
            var txs = _paymentService.GetUserTransactions(username);
            foreach (var tx in txs)
            {
                Console.WriteLine($"{tx.Timestamp}: {tx.Type} ${tx.Amount} from {tx.FromUser} to {tx.ToUser}");
            }
        }
    }
}
