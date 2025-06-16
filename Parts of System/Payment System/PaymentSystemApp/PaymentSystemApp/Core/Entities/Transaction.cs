using System;

namespace PaymentSystemApp.Core.Entities {
    public class Transaction {
        public string FromUser { get; set; }
        public string ToUser { get; set; }
        public decimal Amount { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public string Type { get; set; } // "Send" or "Request"
    }
}