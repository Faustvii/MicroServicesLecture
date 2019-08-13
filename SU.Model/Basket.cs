using Newtonsoft.Json;
using System.Collections.Generic;

namespace SU.Model
{
    public class Basket
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        [JsonIgnore]
        public virtual User User { get; set; }
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}