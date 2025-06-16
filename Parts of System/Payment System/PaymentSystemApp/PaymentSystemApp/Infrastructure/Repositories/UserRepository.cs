// File: Infrastructure/Repositories/UserRepository.cs
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using PaymentSystemApp.Core.Entities;
using PaymentSystemApp.Core.Interfaces;

namespace PaymentSystemApp.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string _filePath;
        private List<User> _users;

        public UserRepository(string filePath)
        {
            _filePath = filePath;
            _users = LoadFromFile();
        }

        public void Add(User user)
        {
            _users.Add(user);
            SaveToFile();
        }

        public IEnumerable<User> GetAll() => _users;

        public User GetByUsername(string username) =>
            _users.FirstOrDefault(u => u.Username == username);

        public void Remove(string username)
        {
            var user = GetByUsername(username);
            if (user != null)
            {
                _users.Remove(user);
                SaveToFile();
            }
        }

        private List<User> LoadFromFile()
        {
            if (!File.Exists(_filePath)) return new List<User>();
            var json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>();
        }

        private void SaveToFile()
        {
            var json = JsonSerializer.Serialize(_users, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, json);
        }
    }
}
