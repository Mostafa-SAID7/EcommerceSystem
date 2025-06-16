using PaymentSystemApp.App;
using PaymentSystemApp.Core.Interfaces;
using PaymentSystemApp.Infrastructure.Repositories;
using PaymentSystemApp.Infrastructure.Services;

class Program
{
    static void Main()
    {
        const string userFile = "users.json";
        const string transactionFile = "transactions.json";

        IUserRepository userRepo = new UserRepository(userFile);
        ITransactionRepository txRepo = new TransactionRepository(transactionFile);

        IUserService userService = new UserService(userRepo);
        IPaymentService paymentService = new PaymentService(txRepo, userRepo);

        MenuManager menu = new MenuManager(userService, paymentService);
        menu.Start();
    }
}