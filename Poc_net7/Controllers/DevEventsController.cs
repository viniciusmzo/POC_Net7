using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Poc_net7.Entities;
using Poc_net7.Persistence;

namespace Poc_net7.Controllers
{
    [Route("api/dev-events")]
    [ApiController]
    public class DevEventsController : ControllerBase
    {
        private readonly DevEventsDbContext _context;
        public DevEventsController(DevEventsDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll() 
        {
            var devEvents = _context.DevEvents.Where(x => !x.IsDeleted).ToList();

            return Ok(devEvents);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id) 
        {
            var devEvent = _context.DevEvents.SingleOrDefault(x => x.Id == id);

            if (devEvent == null) 
            {
                return NotFound();
            }

            return Ok(devEvent);
        }

        [HttpPost]
        public IActionResult Post(DevEvent devEvent) 
        {
            _context.DevEvents.Add(devEvent);

            return CreatedAtAction(nameof(GetById), new { id = devEvent.Id}, devEvent);
        }

        [HttpPut("{id}")]
        public IActionResult Put(Guid id, DevEvent devEvent) 
        {
            var devEventDb = _context.DevEvents.SingleOrDefault(x => x.Id == id);

            if (devEventDb == null)
            {
                return NotFound();
            }

            devEventDb.Update(devEvent.Title, devEvent.Description, devEvent.StartDate, devEvent.EndDate);

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id) 
        {
            var devEventDb = _context.DevEvents.SingleOrDefault(x => x.Id == id);

            if (devEventDb == null)
            {
                return NotFound();
            }

            devEventDb.Delete();

            return Ok();
        }
    }
}
