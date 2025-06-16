using System.Collections.Generic;
using System.Linq;
using PaymentSystemApp.Core.Entities;
using PaymentSystemApp.Core.Interfaces;

namespace PaymentSystemApp.Infrastructure.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly ITransactionRepository _txRepo;
        private readonly IUserRepository _userRepo;

        public PaymentService(ITransactionRepository txRepo, IUserRepository userRepo)
        {
            _txRepo = txRepo;
            _userRepo = userRepo;
        }

        public bool Send(User from, string toUsername, decimal amount)
        {
            var toUser = _userRepo.GetByUsername(toUsername);
            if (toUser == null || from.Username == toUser.Username || from.Balance < amount) return false;

            from.Balance -= amount;
            toUser.Balance += amount;

            var tx = new Transaction
            {
                FromUser = from.Username,
                ToUser = toUser.Username,
                Amount = amount,
                Type = "Send"
            };

            _txRepo.Add(tx);
            return true;
        }

        public IEnumerable<Transaction> GetUserTransactions(string username) =>
            _txRepo.GetAll().Where(t => t.FromUser == username || t.ToUser == username);

        public IEnumerable<Transaction> GetAllTransactions() => _txRepo.GetAll();
    }
}
