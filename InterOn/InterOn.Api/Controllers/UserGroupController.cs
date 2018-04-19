using Microsoft.AspNetCore.Mvc;

namespace InterOn.Api.Controllers
{   [Route("api/group/{groupId}/users")]
    public class UserGroupController : Controller
    {
        public UserGroupController()
        {
            
        }
        /*[HttpPost]
        public async Task<IActionResult> AddUserForGroup(int groupId,int userId)
        {
             ....
        }*/
    }
}