using System.Threading.Tasks;
using InterOn.Data.DbModels;
using Microsoft.AspNetCore.Mvc;

namespace InterOn.Api.Controllers
{   [Route("api/group/{groupId}")]
    public class UserGroupController : Controller
    {
        public UserGroupController()
        {
            
        }
        //[HttpPost]
        //public async Task<IActionResult> AddUserForGroup(int groupId)
        //{
        //    var userId = int.Parse(HttpContext.User.Identity.Name);
        //    var merdz = new UserGroup { GroupId = groupId, UserId = userId };

        //}
    }
}