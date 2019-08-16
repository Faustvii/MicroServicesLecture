using System;

namespace SU.Model
{
    public class BasketModifiedEvent
    {
        public int Id { get; set; }
        public int BasketId { get; set; }
        public int UserId { get; set; }
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
