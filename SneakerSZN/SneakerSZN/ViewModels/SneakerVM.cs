using SneakerSZN_BLL.Models;

namespace SneakerSZN.ViewModels
{
    public class SneakerVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Size { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }

        public int BrandId { get; set; }
        public Brand Brand { get; set; }
    }
}
