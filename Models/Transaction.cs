using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace accounts.Models{
    public class Transaction : BaseEntity
    {
        // public int id { get; set; }
        public decimal amount { get; set; }
        public int accountId { get; set; }
    }
}