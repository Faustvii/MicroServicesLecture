using Microsoft.AspNetCore.Mvc;
using SU.Model;
using SU.Services;
using System.Collections.Generic;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace SU.API.Features.Baskets
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly BasketService _basketService;

        public BasketController(BasketService basketService)
        {
            _basketService = basketService;
        }

        // GET api/basket
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Basket>), Status200OK)]
        public ActionResult<IEnumerable<Basket>> GetAll()
        {
            var baskets = _basketService.GetBaskets();
            return Ok(baskets);
        }

        // GET api/values/5
        [HttpGet("{userId}")]
        [ProducesResponseType(typeof(Basket), Status200OK)]
        [ProducesResponseType(Status404NotFound)]
        public ActionResult<Basket> Get(int userId)
        {
            return _basketService.GetBasket(userId);
        }

        // POST api/values
        [HttpPost]
        [ProducesResponseType(typeof(Basket), Status201Created)]
        public IActionResult Post()
        {
            var basket = _basketService.Create();
            return CreatedAtAction(nameof(Get), new { userId = basket.Id }, basket);
        }

        // PUT api/values/5
        [HttpPost("{userId}")]
        public IActionResult AddProduct(int userId, [FromBody] BasketDTO.Post model)
        {
            _basketService.AddProductToBasket(userId, model.Name);
            return Ok();
        }

        // DELETE api/values/5
        [HttpDelete("{userId}/{productId}")]
        public IActionResult DeleteProduct(int userId, int productId)
        {
            _basketService.RemoveProductFromBasket(userId, productId);
            return NoContent();
        }

        // DELETE api/values/5
        [HttpDelete("{userId}")]
        public IActionResult DeleteBasket(int userId)
        {
            _basketService.Delete(userId);
            return NoContent();
        }
    }
}
