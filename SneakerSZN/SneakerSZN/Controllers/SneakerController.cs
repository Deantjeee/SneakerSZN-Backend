using Microsoft.AspNetCore.Mvc;
using SneakerSZN.RequestModels;
using SneakerSZN.ViewModels;
using SneakerSZN_BLL.Interfaces.Services;
using SneakerSZN_BLL.Models;

namespace SneakerSZN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SneakerController : ControllerBase
    {
        private readonly ISneakerService _sneakerService;

        public SneakerController(ISneakerService sneakerService)
        {
            _sneakerService = sneakerService;
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
            });
        }

        [HttpGet("{id:int}")]
        public SneakerVM Get(int id)
        {
            Sneaker? sneaker = _sneakerService.GetById(id);

            if (sneaker == null)
            {
                return null;
            }

            SneakerVM sneakerVM = new()
            {
                Id = id,
                Name = sneaker.Name,
                Size = sneaker.Size,
                Price = sneaker.Price,
            };

            return sneakerVM;
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult Post([FromBody] SneakerRequest sneakerRequest)
        {
            Sneaker sneaker = new()
            {
                Name = sneakerRequest.Name,
                Size = sneakerRequest.Size,
                Price = sneakerRequest.Price
            };

            if (!_sneakerService.Create(sneaker))
            {
                return BadRequest("Item not created");
            }

            return Ok();
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult Post(int id, [FromBody] SneakerRequest sneakerRequest)
        {
            Sneaker sneaker = new()
            {
                Id = id,
                Name = sneakerRequest.Name,
                Size = sneakerRequest.Size,
                Price = sneakerRequest.Price
            };

            if (!_sneakerService.Update(sneaker))
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
            if (!_sneakerService.Delete(id))
            {
                return BadRequest("Item not deleted");
            }

            return Ok();
        }
    }
}
