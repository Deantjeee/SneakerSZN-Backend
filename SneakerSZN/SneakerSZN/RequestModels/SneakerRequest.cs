using SneakerSZN_BLL.Models;

namespace SneakerSZN.RequestModels
{
    public class SneakerRequest
    {
        public string Name { get; set; }
        public int Size { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int BrandId { get; set; }
    }
}
