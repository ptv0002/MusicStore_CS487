using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Models
{
    [Table("Instrument")]
    public class Instrument
    {
        public Instrument()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }
        [Display(Name = "Instrument ID")]
        public int ID { get; set; }
        public string Brand { get; set; }
        [Display(Name = "Type")]
        public string InstrumentType { get; set; }
        public string Model { get; set; }
        [Display(Name = "Unit Price")]
        public double? UnitPrice { get; set; }
        public int? Quantity { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
