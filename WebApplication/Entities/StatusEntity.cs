using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication.Entities
{
    [Table("statuses")]
    public class StatusEntity : AbstractEntity
    {
        [Column("status")]
        public Status Status { get; set; }
    }
}