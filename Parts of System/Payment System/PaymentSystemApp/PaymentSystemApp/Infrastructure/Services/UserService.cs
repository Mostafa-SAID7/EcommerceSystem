using System.Collections.Generic;
using PaymentSystemApp.Core.Entities;
using PaymentSystemApp.Core.Interfaces;

namespace PaymentSystemApp.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;

        public UserService(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public bool Register(string username, string password)
        {
            if (_userRepo.GetByUsername(username) != null) return false;
            _userRepo.Add(new User
            {
                Username = username,
                Password = password,
                IsAdmin = username.ToLower() == "admin"
            });
            return true;
        }

        public User Login(string username, string password) =>
            _userRepo.GetByUsername(username)?.Password == password ? _userRepo.GetByUsername(username) : null;

        public IEnumerable<User> GetAllUsers() => _userRepo.GetAll();
        public void PromoteToAdmin(string username)
        {
            var user = _userRepo.GetByUsername(username);
            if (user != null) user.IsAdmin = true;
        }

        public void DemoteUser(string username)
        {
            var user = _userRepo.GetByUsername(username);
            if (user != null) user.IsAdmin = false;
        }

        public void DeleteUser(string username) => _userRepo.Remove(username);
    }
}
