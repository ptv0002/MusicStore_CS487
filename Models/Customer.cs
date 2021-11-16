using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Models
{
    [Table("Customer")]
    public class Customer
    {
        public Customer()
        {
            OrderInfos = new HashSet<OrderInfo>();
        }
        public int ID { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public virtual ICollection<OrderInfo> OrderInfos { get; set; }
    }
}
