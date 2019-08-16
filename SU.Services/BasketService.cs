using SU.Model;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;

namespace SU.Services
{
    public class BasketService
    {
        private readonly BasketContext _context;

        public BasketService(BasketContext context)
        {
            _context = context;
        }

        public IEnumerable<Basket> GetBaskets()
        {
            return _context.Baskets.Include(x => x.Products).ToList();
        }

        public Basket GetBasket(int userId)
        {
            return _context.Baskets.Include(x => x.Products).FirstOrDefault(x => x.UserId == userId);
        }

        public Basket Create(int? userId = null)
        {
            var user = new User();
            if (userId.HasValue)
            {
                user.Id = userId.Value;
            }
            var basket = new Basket
            {
                User = user
            };
            _context.Add(basket);
            _context.SaveChanges();
            //basket.User = null;
            return basket;
        }

        public void Delete(int userId)
        {
            var basket = GetBasket(userId);
            if (basket != null)
            {
                _context.Remove(basket);
                _context.SaveChanges();
            }
        }

        public void AddProductToBasket(int userId, string productName)
        {
            var basket = GetBasket(userId);
            if (basket != null)
            {
                var product = new Product
                {
                    Name = productName
                };


                basket.Products.Add(product);
                var eventMessage = $"Added {product.Name} to basket - {basket.Products.Count} products in basket";
                _context.BasketModifiedEvents.Add(CreateEvent(userId, basket.Id, eventMessage));

                _context.SaveChanges();
            }
        }

        private BasketModifiedEvent CreateEvent(int userId, int basketId, string message)
        {
            return new BasketModifiedEvent
            {
                BasketId = basketId,
                UserId = userId,
                Message = message,
                Timestamp = DateTime.Now
            };
        }

        public void RemoveProductFromBasket(int userId, int productId)
        {
            var basket = GetBasket(userId);
            var updatedProducts = basket.Products.Where(x => x.Id != productId).ToList();
            var eventMessage = $"Removed {basket.Products.First(x => x.Id == productId).Name} from basket - {updatedProducts.Count} products in basket";
            basket.Products = updatedProducts;
            _context.BasketModifiedEvents.Add(CreateEvent(userId, basket.Id, eventMessage));

            _context.SaveChanges();
        }
    }
}
