using System.Collections.Generic;

namespace SU.Model
{
    public class User
    {
        public int Id { get; set; }
        public virtual IEnumerable<Basket> Baskets { get; set; }
    }
}