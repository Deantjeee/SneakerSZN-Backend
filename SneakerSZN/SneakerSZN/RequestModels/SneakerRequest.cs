using SneakerSZN_BLL.Models;
using System.ComponentModel.DataAnnotations;

namespace SneakerSZN.RequestModels
{
    public class SneakerRequest
    {
        [Required(ErrorMessage = "Sneaker name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Sneaker size is required.")]
        public int Size { get; set; }

        [Required(ErrorMessage = "Sneaker price is required.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Sneaker stock is required.")]
        public int Stock { get; set; }

        public int BrandId { get; set; }
    }
}
