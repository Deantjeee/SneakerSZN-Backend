using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SneakerSZN.RequestModels;
using SneakerSZN.ViewModels;
using SneakerSZN_BLL.Interfaces.Services;
using SneakerSZN_BLL.Models;
using SneakerSZN_BLL.Services;

namespace SneakerSZN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IBrandService _brandService;

        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        [HttpGet]
        public IEnumerable<BrandVM> Get()
        {
            return _brandService.GetAll().Select(brand => new BrandVM
            {
                Id = brand.Id,
                Name = brand.Name,
            });
        }

        [HttpGet("{id:int}")]
        public BrandVM Get(int id)
        {
            Brand? brand = _brandService.GetById(id);

            if (brand == null)
            {
                return null;
            }

            BrandVM brandVM = new()
            {
                Id = id,
                Name = brand.Name,
            };

            return brandVM;
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Post([FromBody] BrandRequest brandRequest)
        {
            Brand brand = new()
            {
                Name = brandRequest.Name
            };

            if (!_brandService.Create(brand))
            {
                return BadRequest("Item not created");
            }

            return Ok();
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult Post(int id, [FromBody] BrandRequest brandRequest)
        {
            Brand brand = new()
            {
                Id = id,
                Name = brandRequest.Name
            };

            if (!_brandService.Update(brand))
            {
                return BadRequest("Item not created");
            }

            return Ok();
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult Delete(int id)
        {
            if (!_brandService.Delete(id))
            {
                return BadRequest("Item not deleted");
            }

            return Ok();
        }
    }
}
