using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Music_Web.Models
{
    public class OrderViewModel
    {
        public int ID { get; set; }
        public int CustomerID { get; set; }
        [Display(Name = "Customer Name")]
        public string CFirstName { get; set; }
        public string CLastName { get; set; }
        public string CPhone { get; set; }
        [Display(Name = "Employee ID")]
        public int EmployeeID { get; set; }
        [Display(Name = "Employee Name")]
        public string EFirstName { get; set; }
        [Display(Name = "Last Name")]
        public string ELastName { get; set; }
        [Display(Name = "Total Order's Value")]
        public double? TotalOrder { get; set; }
        [Display(Name = "Date of Order")]
        public DateTime? DateOfOrder { get; set; }
        public IEnumerable<OrderDetailViewModel> OrderDetails { get; set; }
    }
}
