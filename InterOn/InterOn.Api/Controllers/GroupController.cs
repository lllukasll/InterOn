using System;
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
        public async Task<IActionResult> CreateGroup([FromBody] GroupDto groupDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var group = _mapper.Map<GroupDto, Group>(groupDto);
             _groupRepository.AddAsync(group);
            group.CreateDateTime = DateTime.Now;
            await _unitOfWork.CompleteAsync();

            var result = _mapper.Map<Group, GroupDto>(group);

            return Ok(result);
        }
     
    }
}