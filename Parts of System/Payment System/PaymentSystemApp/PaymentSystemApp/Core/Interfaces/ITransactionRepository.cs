using System.Collections.Generic;
using PaymentSystemApp.Core.Entities;

namespace PaymentSystemApp.Core.Interfaces {
    public interface ITransactionRepository {
        void Add(Transaction transaction);
        IEnumerable<Transaction> GetAll();
    }
}