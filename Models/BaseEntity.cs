using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace accounts.Models{
    public abstract class BaseEntity{
        public BaseEntity(){
            created_at = DateTime.Now;
        }
        public int id { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
    }
}