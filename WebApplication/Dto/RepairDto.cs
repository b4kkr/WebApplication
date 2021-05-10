using System;

namespace WebApplication.Dto
{
    public class RepairDto
    {
        public Guid RepairGuid { get; set; }
        public string Status { get; set; }
        public CarDto CarDto { get; set; }
        public UserDto UserDto { get; set; }
        public string Works { get; set; }
        public int Price { get; set; }
        public bool Paid { get; set; }
    }
}