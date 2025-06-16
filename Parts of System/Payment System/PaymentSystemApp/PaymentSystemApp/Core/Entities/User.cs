namespace PaymentSystemApp.Core.Entities
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public decimal Balance
        {
            get => IsAdmin ? 0 : _balance;
            set
            {
                if (!IsAdmin) _balance = value;
            }
        }
        private decimal _balance = 1000;
        public bool IsAdmin { get; set; } = false;
    }
}