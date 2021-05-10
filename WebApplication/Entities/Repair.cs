using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication.Entities
{
    [Table("repairs")]
    public class Repair : AbstractEntity
    {
        public User User { get; set; }

        public Car Car { get; set; }
        
        public Status Status { get; set; }
        [Column("works")]
        public string Works { get; set; }
        [Column("price")]
        public int Price { get; set; }
        [Column("paid")]
        public bool Paid { get; set; }
        [Column("guid")]
        public Guid Guid { get; set; }
        
        public long CarId { get; set; }
        public long UserId { get; set; }
        public long StatusId { get; set; }
    }
}