using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using InterOn.Data.DbModels;
using InterOn.Data.ModelsDto;
using InterOn.Repo.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InterOn.Api.Controllers
{
    [Route("group")]
    public class GroupController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGroupRepository _groupRepository;

        public GroupController(IMapper mapper,IGroupRepository groupRepository,IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _groupRepository = groupRepository;
            _unitOfWork = unitOfWork;

        }
        [HttpPost]
        public async Task<IActionResult> CreateGroup([FromBody] CreateGroupDto groupDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var group = _mapper.Map<CreateGroupDto, Group>(groupDto);
            group.CreateDateTime = DateTime.Now;
            _groupRepository.AddAsync(group);
            await _unitOfWork.CompleteAsync();

            var result = _mapper.Map<Group, CreateGroupDto>(group);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGroup(int id,[FromBody] UpdateGroupDto groupDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var group = await _groupRepository.GetGroup(id);
            if (group == null)
                return NotFound();

             _mapper.Map(groupDto,group);

            await _unitOfWork.CompleteAsync();

            group = await _groupRepository.GetGroup(group.Id);
            var result = _mapper.Map<Group, UpdateGroupDto>(group);

            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGroup(int id)
        {
            var group = await _groupRepository.GetGroup(id);
            if (group == null)
                return NotFound();
            
            var resultMap = _mapper.Map<Group, UpdateGroupDto>(group);
            return Ok(resultMap);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGroup(int id)
        {
            var group = await _groupRepository.GetGroup(id, includeRelated: false);
            if (group == null)
                return NotFound();

            _groupRepository.Remove(group);
            await _unitOfWork.CompleteAsync();
            return Ok(id);
        } 
    }
}