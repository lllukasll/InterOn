﻿using System.Threading.Tasks;
using InterOn.Data.ModelsDto.Event;
using InterOn.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InterOn.Api.Controllers
{   [Authorize]
    [Route("api/group/{groupId}/event")]
    public class EventGroupController : Controller
    {
        private readonly IEventService _service;

        public EventGroupController(IEventService service)
        {
            _service = service;
        }
        [HttpPost]
        public async Task<IActionResult> CreateEventForGroup(int groupId,[FromBody] CreateEventDto eventDto)
        {
            if (await _service.ExistGroup(groupId) == false)
                return BadRequest("Nie ma grupy o tym Id");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var userId = int.Parse(HttpContext.User.Identity.Name);
            await _service.CreateEventForGroupAsync(eventDto, groupId,userId);
            return Ok();
        }
    }
}