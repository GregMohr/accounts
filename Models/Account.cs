using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace accounts.Models{
    public class Account : BaseEntity
    {
        public Account(){
            transactions = new List<Transaction>();
        }
        // public int id { get; set; }
        public string type { get; set; }
        public decimal balance { get; set; }
        public int userId { get; set; }
        public ICollection<Transaction> transactions { get; set; }
    }
}