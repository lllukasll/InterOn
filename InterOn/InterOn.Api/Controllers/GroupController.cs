using System;
using System.Threading.Tasks;
using AutoMapper;
using InterOn.Data.DbModels;
using InterOn.Data.ModelsDto.Group;
using InterOn.Repo.Interfaces;
using InterOn.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InterOn.Api.Controllers
{
    [Route("/api/group")]
    public class GroupController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGroupService _groupRepository;

        public GroupController(IMapper mapper,IGroupService groupRepository,IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _groupRepository = groupRepository;
            _unitOfWork = unitOfWork;
        }
        [HttpPost]
        public async Task<IActionResult> CreateGroup([FromBody] CreateGroupDto groupDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var group = _mapper.Map<CreateGroupDto, Group>(groupDto);
                group.CreateDateTime = DateTime.Now;
                await _groupRepository.AddAsync(group);
                await _unitOfWork.CompleteAsync();

                var result = _mapper.Map<Group, CreateGroupDto>(group);

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest($"{e.Message} {e.InnerException}");
            }
           
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGroup(int id,[FromBody] UpdateGroupDto groupDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var group = await _groupRepository.GetGroupAsync(id);
                if (group == null)
                    return NotFound();

                _mapper.Map(groupDto, group);

                await _unitOfWork.CompleteAsync();

                group = await _groupRepository.GetGroupAsync(group.Id);
                var result = _mapper.Map<Group, UpdateGroupDto>(group);

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest($"{e.InnerException},{e.Message}");
            }  
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGroup(int id)
        {
            try
            {
                var group = await _groupRepository.GetGroupAsync(id);
                if (group == null)
                    return NotFound();

                var resultMap = _mapper.Map<Group, CreateGroupDto>(group);
                return Ok(resultMap);
            }
            catch (Exception e)
            {
                return BadRequest($"{e.InnerException},{e.Message}");
            }
           
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGroup(int id)
        {
            var group = await _groupRepository.GetGroupAsync(id, includeRelated: false);
            if (group == null)
                return NotFound();

            _groupRepository.Remove(group);
            await _unitOfWork.CompleteAsync();
            return Ok(id);
        }
    }
}