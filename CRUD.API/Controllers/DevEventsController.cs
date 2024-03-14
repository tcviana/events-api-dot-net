using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CRUD.API.Entities;
using CRUD.API.Persistence;

namespace CRUD.API.Controllers

{
    [Route("api/dev-events")]
    [ApiController]
    public class DevEventsController : ControllerBase
    {

        private readonly DevEventsDbContent _content;

        public DevEventsController(DevEventsDbContent content)
        {
            _content = content;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var devEvents = _content.DevEvents.Where(d => !d.IsDeleted).ToList();

            return Ok(devEvents);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var devEvent = _content.DevEvents.SingleOrDefault(d => d.Id == id);

            if (devEvent == null)
            {
                return NotFound();
            }
            return Ok(devEvent);
        }
        [HttpPost]
        public IActionResult Post(DevEvent devEvent)
        {
            _content.DevEvents.Add(devEvent);
            return CreatedAtAction(nameof(GetById), new { id = devEvent.Id }, devEvent);
        }
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, DevEvent input)
        {
            var devEvent = _content.DevEvents.SingleOrDefault(d => d.Id == id);

            if (devEvent == null)
            {
                return NotFound();
            }

            devEvent.Update(input.Title, input.Description, input.StartDate, input.EndDate);

            return NoContent();
        }

        [HttpDelete]
        public IActionResult Delete(Guid Id)
        {
            var devEvent = _content.DevEvents.SingleOrDefault(d => d.Id == Id);

            if (devEvent == null || devEvent.Id == Guid.Empty)
            {
                return NotFound();
            }

            devEvent.Delete();

            return NoContent();

        }
    }
}

