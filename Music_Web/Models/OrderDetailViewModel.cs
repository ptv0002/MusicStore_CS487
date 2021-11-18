using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Music_Web.Models
{
    public class OrderDetailViewModel
    {
        public int? ID { get; set; }
        public int? OrderInfoID { get; set; }
        public int? InstrumentID { get; set; }
        public string Brand { get; set; }
        public string Type { get; set; }
        public string Model { get; set; }
        [Display(Name = "Quantity Sold")]
        public int? QuantitySold { get; set; }
        [Display(Name = "Unit Price")]
        public double? UnitPrice { get; set; }
        [Display(Name = "Total Price")]
        public double? TotalPrice { get; set; }
    }
}
