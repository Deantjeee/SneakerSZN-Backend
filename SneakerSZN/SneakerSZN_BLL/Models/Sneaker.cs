using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SneakerSZN_BLL.Models
{
    public class Sneaker
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Size { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public byte[] Image { get; set; }

        public int BrandId { get; set; }
        public Brand Brand { get; set; }
    }
}
