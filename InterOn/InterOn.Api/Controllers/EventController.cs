using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using InterOn.Data.ModelsDto.Event;
using InterOn.Service.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace InterOn.Api.Controllers
{
    [Route("api/event")]
    public class EventController : Controller
    {
        private readonly IEventService _service;
        private readonly IHostingEnvironment _hosting;

        public EventController(IEventService service,IHostingEnvironment hosting)
        {
            _service = service;
            _hosting = hosting;
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvent([FromBody] CreateEventDto eventDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await _service.CreateEventAsync(eventDto);
            
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEvent(int id, [FromBody] UpdateEventDto eventDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (await _service.ExistEvent(id) == false)
            {
                return NotFound();
            }

            var result = await _service.UpdateEventAsync(id, eventDto);

            return Ok(result);
        }
    }
}