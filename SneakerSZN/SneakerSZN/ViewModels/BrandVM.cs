using SneakerSZN_BLL.Models;

namespace SneakerSZN.ViewModels
{
    public class BrandVM
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Sneaker>? Sneakers { get; set; }
    }
}
