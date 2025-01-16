using SneakerSZN_BLL.Models;
using System.ComponentModel.DataAnnotations;

namespace SneakerSZN.RequestModels
{
    public class BrandRequest
    {
        [Required(ErrorMessage = "Brand name is required.")]
        [StringLength(20, ErrorMessage = "Brand name cannot exceed 20 characters.")]
        public string Name { get; set; }

    }
}
