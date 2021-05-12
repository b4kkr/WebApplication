using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication.Entities
{
    [Table("repairs")]
    public class Repair : AbstractEntity
    {
        
        [ForeignKey("car_id")]
        public Car Car { get; set; }
        [ForeignKey("status_id")]
        public virtual StatusEntity StatusEntity { get; set; }
        [Column("works")]
        public string Works { get; set; }
        [Column("price")]
        public int Price { get; set; }
        [Column("paid")]
        public bool Paid { get; set; }
        [Column("guid")]
        public Guid Guid { get; set; }

    }
}