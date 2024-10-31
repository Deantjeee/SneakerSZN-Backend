using Microsoft.AspNetCore.Mvc;
using SneakerSZN.RequestModels;
using SneakerSZN.ViewModels;
using SneakerSZN_BLL.Interfaces.Services;
using SneakerSZN_BLL.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SneakerSZN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SneakerController : ControllerBase
    {
        private readonly ISneakerService _sneakerService;
        private readonly IBrandService _brandService;

        public SneakerController(ISneakerService sneakerService, IBrandService brandService)
        {
            _sneakerService = sneakerService;
            _brandService = brandService;
        }

        [HttpGet]
        public IEnumerable<SneakerVM> Get()
        {
            return _sneakerService.GetAll().Select(sneaker => new SneakerVM
            {
                Id = sneaker.Id,
                Name = sneaker.Name,
                Size = sneaker.Size,
                Price = sneaker.Price,
                Stock = sneaker.Stock,
                BrandId = sneaker.BrandId,
                Brand = _brandService.GetById(sneaker.BrandId),
                Image = sneaker.Image
            });
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult<SneakerVM> Get(int id)
        {
            Sneaker? sneaker = _sneakerService.GetById(id);

            if (sneaker == null)
            {
                return NotFound();
            }

            return new SneakerVM
            {
                Id = id,
                Name = sneaker.Name,
                Size = sneaker.Size,
                Price = sneaker.Price,
                Stock = sneaker.Stock,
                BrandId = sneaker.BrandId,
                Brand = _brandService.GetById(sneaker.BrandId),
                Image = sneaker.Image
            };
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> Post([FromForm] SneakerRequest sneakerRequest, IFormFile imageFile)
        {
            Brand? brand = _brandService.GetById(sneakerRequest.BrandId);

            if (brand == null)
            {
                return BadRequest("No brand");
            }

            Sneaker sneaker = new()
            {
                Name = sneakerRequest.Name,
                Size = sneakerRequest.Size,
                Price = sneakerRequest.Price,
                Stock = sneakerRequest.Stock,
                BrandId = sneakerRequest.BrandId,
                Brand = brand
            };

            if (imageFile != null && imageFile.Length > 0)
            {
                using var memoryStream = new MemoryStream();
                await imageFile.CopyToAsync(memoryStream);
                sneaker.Image = memoryStream.ToArray();
            }

            if (!_sneakerService.Create(sneaker))
            {
                return BadRequest("Sneaker not created");
            }

            return Ok();
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Put(int id, [FromForm] SneakerRequest sneakerRequest, IFormFile imageFile)
        {
            Sneaker? existingSneaker = _sneakerService.GetById(id);

            if (existingSneaker == null)
            {
                return NotFound();
            }

            existingSneaker.Name = sneakerRequest.Name;
            existingSneaker.Size = sneakerRequest.Size;
            existingSneaker.Price = sneakerRequest.Price;
            existingSneaker.Stock = sneakerRequest.Stock;
            existingSneaker.BrandId = sneakerRequest.BrandId;

            if (imageFile != null && imageFile.Length > 0)
            {
                using var memoryStream = new MemoryStream();
                await imageFile.CopyToAsync(memoryStream);
                existingSneaker.Image = memoryStream.ToArray();
            }

            if (!_sneakerService.Update(existingSneaker))
            {
                return BadRequest("Sneaker not updated");
            }

            return Ok();
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult Delete(int id)
        {
            if (!_sneakerService.Delete(id))
            {
                return BadRequest("Sneaker not deleted");
            }

            return Ok();
        }
    }
}
