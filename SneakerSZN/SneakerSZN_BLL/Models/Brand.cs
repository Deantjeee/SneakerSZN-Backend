using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SneakerSZN_BLL.Models
{
    public class Brand
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        [JsonIgnore]
        public ICollection<Sneaker>? Sneakers { get; set; }

        public Brand() { }
    }
}
