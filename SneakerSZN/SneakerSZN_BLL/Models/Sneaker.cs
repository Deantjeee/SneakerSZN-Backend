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
        public double Price { get; set; }
        public int Stock { get; set; }
        public byte[]? Image { get; set; }

        public int? BrandId { get; set; }
        public Brand? Brand { get; set; }

        public Sneaker() 
        { 
        }

        public Sneaker(int id, string name, int size, double price, int stock, byte[]? image, int? brandId, Brand? brand)
        {
            Id = id;
            Name = name;
            Size = size;
            Price = price;
            Stock = stock;
            Image = image;
            BrandId = brandId;
            Brand = brand;
        }
    }
}
