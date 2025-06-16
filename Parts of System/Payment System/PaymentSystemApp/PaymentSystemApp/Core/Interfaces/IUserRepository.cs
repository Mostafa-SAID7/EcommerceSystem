using System.Collections.Generic;
using PaymentSystemApp.Core.Entities;

namespace PaymentSystemApp.Core.Interfaces
{
    public interface IUserRepository
    {
        User GetByUsername(string username);
        IEnumerable<User> GetAll();
        void Add(User user);
        void Remove(string username);
    }   
}
