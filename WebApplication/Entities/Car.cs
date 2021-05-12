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
        [ForeignKey("user_id")]
        public User User { get; set; }
        [ForeignKey("car_type_id")]
        public CarType Type { get; set; }
        
        [InverseProperty("Car")]
        public Repair Repair { get; set; }

    }
}