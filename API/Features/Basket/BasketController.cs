using System.Collections.Generic;
using System.Linq;
using MicroServicesLecture.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MicroServicesLecture.API.Features.Baskets
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly MicroServicesLectureContext _context;

        public BasketController(MicroServicesLectureContext context)
        {
            _context = context;
        }

        // GET api/basket
        [HttpGet]
        public ActionResult<IEnumerable<Basket>> Get()
        {
            return _context.Baskets.Include(x => x.Products).ToList();
        }

        // GET api/values/5
        [HttpGet("{userId}")]
        public ActionResult<Basket> Get(int userId)
        {
            return _context.Baskets.Include(x => x.Products).FirstOrDefault(x => x.UserId == userId);
        }

        // POST api/values
        [HttpPost]
        public void Post()
        {
            var basket = new Basket()
            {
                User = new User()
            };
            _context.Add(basket);
            _context.SaveChanges();
        }

        // PUT api/values/5
        [HttpPut("{userId}")]
        public void Put(int userId, [FromBody] BasketDTO.Put model)
        {
            var basket = _context.Baskets.FirstOrDefault(x => x.UserId == userId);
            if (basket != null)
            {
                var product = new Product
                {
                    Name = model.Name
                };

                basket.Products.Add(product);
                _context.SaveChanges();
            }
        }

        // DELETE api/values/5
        [HttpDelete("{userId}/{productId}")]
        public void DeleteProductFromBasket(int userId, int productId)
        {
            var basket = _context.Baskets.Include(x => x.Products).FirstOrDefault(x => x.UserId == userId);
            var product = basket.Products.FirstOrDefault(x => x.Id == productId);

            basket.Products.Remove(product);

            _context.SaveChanges();
        }

        // DELETE api/values/5
        [HttpDelete("{userId}")]
        public void DeleteBasket(int userId)
        {
            _context.RemoveRange(_context.Baskets.Where(x => x.UserId == userId));
            _context.SaveChanges();
        }
    }
}
