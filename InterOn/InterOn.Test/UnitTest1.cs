using InterOn.Data.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using Xunit;
using InterOn.Repo.Interfaces;
using InterOn.Service.Interfaces;
using InterOn.Api.Controllers;
using InterOn.Api.Helpers;
using InterOn.Repo;
using InterOn.Service.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestPlatform.CrossPlatEngine.DataCollection;

namespace InterOn.Test
{
    public class UnitTest1
    {
        private readonly IMapper _mapper;
        private readonly IOptions<AppSettings> _appSettings;
        private readonly IUnitOfWork _unitOfWork;

        [Fact]
        public void Test1()
        {

            //var users = new List<User>()
            //{
            //    new User(),
            //    new User(),
            //    new User()
            //};

            //var usersRepository = new Mock<IRepository<User>>();
            //var roleRepo = new Mock<IRepository<Role>>();
            //var usersService = new UserService(usersRepository, roleRepo);

            //usersService.Setup(x => x.GetAllUsers()).Returns(users.AsEnumerable);
            //var userController = new UsersController(usersService.Object, _mapper, _appSettings);

            //var result = userController.GetUsers();
            //var okResult = Assert.IsType<OkObjectResult>(result);

            //var resultValueUsers = Assert.IsAssignableFrom<IEnumerable<User>>(okResult.Value);
            //Assert.Equal(3, resultValueUsers.Count());
        }
        [Fact]
        public void test2()
        {
            //var group = new Group
            //{
            //    Id = 1
            //};
            //var id = 1;


            //var groupService = new Mock<IGroupService>();
            //groupService
            //    .Setup(x => x.GetGroupAsync(It.IsAny<int>(),false)).ReturnsAsync(groups);
            //var controller = new GroupController(_mapper, groupService.Object, _unitOfWork);
            //var result = controller.GetGroup(id)
        }
    }
}
