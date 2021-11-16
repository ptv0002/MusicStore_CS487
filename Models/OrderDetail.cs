using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Models
{
    [Table("OrderDetail")]
    public class OrderDetail
    {
        public int? ID { get; set; }
        [Display(Name = "Order Info ID")]
        public int? OrderInfoID { get; set; }
        [Display(Name = "Instrument ID")]
        public int? InstrumentID { get; set; }
        [Display(Name = "Quantity Sold")]
        public int? QuantitySold { get; set; }
        [Display(Name = "Unit Price")]
        public double? UnitPrice { get; set; }
        public virtual Instrument Instrument { get; set; }
        public virtual OrderInfo OrderInfo { get; set; }
    }
}
