using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication.Entities
{
    [Table("car_types")]
    public class CarType : AbstractEntity
    {
        public string TypeName { get; set; }
        public string Model { get; set; }
        
        public List<Car> Cars { get; set; }
    }
}