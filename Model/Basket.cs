using System.Linq;
using System;
using System.Collections.Generic;

namespace MicroServicesLecture.Model
{
    public class Basket
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}