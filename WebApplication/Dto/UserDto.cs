using System;

namespace WebApplication.Dto
{
    public class UserDto
    {
        
        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string RepairGuid { get; set; }
    }
}