// File: Infrastructure/Repositories/TransactionRepository.cs
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using PaymentSystemApp.Core.Entities;
using PaymentSystemApp.Core.Interfaces;

namespace PaymentSystemApp.Infrastructure.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly string _filePath;
        private List<Transaction> _transactions;

        public TransactionRepository(string filePath)
        {
            _filePath = filePath;
            _transactions = LoadFromFile();
        }

        public void Add(Transaction transaction)
        {
            _transactions.Add(transaction);
            SaveToFile();
        }

        public IEnumerable<Transaction> GetAll() => _transactions;

        private List<Transaction> LoadFromFile()
        {
            if (!File.Exists(_filePath)) return new List<Transaction>();
            var json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<Transaction>>(json) ?? new List<Transaction>();
        }

        private void SaveToFile()
        {
            var json = JsonSerializer.Serialize(_transactions, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, json);
        }
    }
}
