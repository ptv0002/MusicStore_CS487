using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Models
{
    [Table("OrderInfo")]
    public class OrderInfo
    {
        public OrderInfo()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int ID { get; set; }
        [Display(Name = "Employee ID")]
        public int? EmployeeID { get; set; }
        [Display(Name = "Customer ID")]
        public int? CustomerID { get; set; }
        [Display(Name = "Date of Order")]
        public DateTime? DateOfOrder { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
