using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace accounts.Models
{
    public class User : BaseEntity
    {
        public User(){
            // created_at = DateTime.Now;
            accounts = new List<Account>();
        }
        // public int id { get; set; }
        [Display(Name="First Name")]
        [Required]
        [MinLength(2)]
        public string first { get; set; }

        [Display(Name="Last Name")]
        [Required]
        [MinLength(2)]
        public string last { get; set; }

        [Display(Name="Email")]
        [Required]
        [EmailAddress]
        public string email { get; set; }

        [Display(Name="Password")]
        [Required]
        [MinLength(8)]        
        [DataType(DataType.Password)]
        public string password { get; set; }
        // public DateTime created_at { get; set; }
        // public DateTime updated_at { get; set; }
        public ICollection<Account> accounts { get; set; }
    }
}