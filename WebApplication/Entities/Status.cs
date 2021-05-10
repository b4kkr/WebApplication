using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication.Entities
{
    [Table("statuses")]
    public class Status : AbstractEntity
    {
        [Column("description")]
        public string Description { get; set; }
        
        public List<Repair> Repairs { get; set; }
    }
}