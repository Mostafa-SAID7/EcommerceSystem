using System.Collections.Generic;
using PaymentSystemApp.Core.Entities;

namespace PaymentSystemApp.Core.Interfaces
{
    public interface IUserService
    {
        bool Register(string username, string password);
        User Login(string username, string password);
        IEnumerable<User> GetAllUsers();
        void PromoteToAdmin(string username);
        void DemoteUser(string username);
        void DeleteUser(string username);
    }
}
