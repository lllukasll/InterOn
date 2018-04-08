using AutoMapper;
using InterOn.Data.DbModels;
using InterOn.Data.ModelsDto;
using InterOn.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace InterOn.Api.Controllers
{
    [Authorize]
    [Route("roles")]
    public class RolesController : Controller
    {
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;

        public RolesController(IRoleService roleService, IMapper mapper)
        {
            _roleService = roleService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var roles = _roleService.GetAll();

            return Ok(roles);
        }

        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var role = _roleService.GetRole(id);

            if (role == null)
                return BadRequest();

            return Ok(role);
        }

        [HttpPost]
        public IActionResult Create([FromBody]RoleDto role)
        {
            if (role == null)
                return BadRequest();

            var roleMapped = _mapper.Map<RoleDto, Role>(role);

            _roleService.InsertRole(roleMapped);

            return Ok(roleMapped);
        }
    }
}
