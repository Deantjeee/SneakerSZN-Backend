using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SneakerSZN_BLL.Models
{
    public class Brand
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        public ICollection<Sneaker>? Sneakers { get; set; }

        public Brand() { }

        public Brand(int id, string name, ICollection<Sneaker>? sneakers)
        {
            Id = id;
            Name = name;
            Sneakers = sneakers;
        }
    }
}
