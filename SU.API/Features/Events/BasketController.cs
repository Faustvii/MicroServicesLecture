using Microsoft.AspNetCore.Mvc;
using SU.Model;
using System.Collections.Generic;
using System.Linq;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace SU.API.Features.Events
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly BasketContext _context;

        public EventController(BasketContext context)
        {
            _context = context;
        }
        
        [HttpGet("")]
        [ProducesResponseType(typeof(IEnumerable<BasketModifiedEvent>), Status200OK)]
        public ActionResult<IEnumerable<BasketModifiedEvent>> Get(int start, int count)
        {
            return Ok(_context.BasketModifiedEvents.Where(x => x.Id > start).Take(count));
        }
        
    }
}
