using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SneakerSZN_BLL.Models
{
    public class Sneaker
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Size { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int Stock { get; set; }
        public byte[] Image { get; set; }

        public int BrandId { get; set; }
        public Brand Brand { get; set; }

        public Sneaker() 
        { 
        }
    }
}
