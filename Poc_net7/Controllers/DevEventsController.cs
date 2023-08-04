using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            var devEvent = _context.DevEvents
                .Include(x => x.Speakers)
                .SingleOrDefault(x => x.Id == id);

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

            _context.SaveChanges();

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

            _context.DevEvents.Update(devEventDb);
            _context.SaveChanges();

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

            _context.SaveChanges();

            return Ok();
        }

        [HttpPost("{id}/speakers")]
        public IActionResult PostSpeaker(Guid id, DevEventSpeaker speaker) 
        {
            speaker.Id = id;

            var devEvent = _context.DevEvents.Any(x => x.Id == id);

            if (!devEvent)
            {
                return NotFound();
            }

            _context.DevEventSpeakers.Add(speaker);
            _context.SaveChanges();

            return Ok();
        }
    }
}
