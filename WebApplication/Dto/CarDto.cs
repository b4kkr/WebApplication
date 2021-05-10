using System;

namespace WebApplication.Dto
{
    public class CarDto
    {
        public long Id { get; set; }
        public CarTypeDto Type { get; set; }
        public string LicensePlate { get; set; }
        public string EngineSerial { get; set; }
        public DateTime Vintage { get; set; }
    }
}