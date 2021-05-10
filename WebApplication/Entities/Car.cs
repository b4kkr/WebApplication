using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication.Entities
{
    [Table("cars")]
    public class Car : AbstractEntity
    {
        [Column("license_plate")]
        public string LicensePlate { get; set; }
        [Column("vintage")]
        public DateTime Vintage { get; set; }
        [Column("engine_serial")]
        public string EngineSerial { get; set; }
        [Column("user")]
        public User User { get; set; }
        [Column("type")]
        public CarType Type { get; set; }
        [Column("repair")]
        public Repair Repair { get; set; }
        
        public long UserId { get; set; }
        public long RepairId { get; set; }
        public long CarTypeId { get; set; }
    }
}