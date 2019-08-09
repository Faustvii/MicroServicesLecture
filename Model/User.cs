using System.Collections.Generic;
namespace MicroServicesLecture.Model
{
    public class User
    {
        public int Id { get; set; }
        public IEnumerable<Basket> Baskets { get; set; }
    }
}