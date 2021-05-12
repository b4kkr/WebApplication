using System;

namespace WebApplication.Dto
{
    public class RepairDto
    {
        public string RepairGuid { get; set; }
        public string Status { get; set; }
        public CarDto Car { get; set; }
        public string Works { get; set; }
        public int Price { get; set; }
        public bool? Paid { get; set; }
    }
}