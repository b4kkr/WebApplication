using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace WebApplication.Entities
{
    [Table("users")]
    public class User : AbstractEntity
    {
        [Column("name")]
        public string Name { get; set; }
        [Column("email")]
        public string Email { get; set; }
        [Column("password")]
        public string Password { get; set; }
        [Column("phone_number")]
        public string PhoneNumber { get; set; }
        [InverseProperty("User")]
        public virtual Car Car { get; set; }
    }
}