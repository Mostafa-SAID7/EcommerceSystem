using System.Collections.Generic;
using PaymentSystemApp.Core.Entities;

namespace PaymentSystemApp.Core.Interfaces {
    public interface IPaymentService {
        bool Send(User from, string toUsername, decimal amount);
        IEnumerable<Transaction> GetUserTransactions(string username);
        IEnumerable<Transaction> GetAllTransactions();
    }
}