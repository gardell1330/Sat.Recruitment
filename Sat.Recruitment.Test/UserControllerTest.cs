using System;
using System.Dynamic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.Api.Models;
using Sat.Recruitment.Common;
using Sat.Recruitment.Common.Enums;
using Xunit;

namespace Sat.Recruitment.Test
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class UserControllerTest
    {
        private Mock<IUserRepository> _mockRepository;
        private Mock<IUserService> _mockService;
        private Mock<IMapper> _mockMapper;
        private UsersController _usersController;

        [Fact]
        public async Task createuser_success()
        {
            SetupUserController();
            
            var result = await _usersController.CreateUser(new UserRequestModel());

            Assert.True(result.IsSuccess);
            Assert.Equal("User Created", result.Response);
        }

        [Fact]
        public async Task createuser_duplicated()
        {
            SetupUserController();
            _mockRepository.Setup(r => r.SaveChanges()).Throws(new Exception("The user is duplicated"));
            var result = await _usersController.CreateUser(new UserRequestModel());

            Assert.False(result.IsSuccess);
            Assert.Equal("The user is duplicated", result.Response);
        }

        private void SetupUserController()
        {
            _mockService = new Mock<IUserService>();
            _mockMapper = new Mock<IMapper>();
            _mockMapper.Setup(r => r.Map<User>(It.IsAny<UserRequestModel>()))
                .Returns(new User());
                
            _mockRepository = new Mock<IUserRepository>();
            _usersController = new UsersController(_mockMapper.Object, _mockService.Object, _mockRepository.Object);
        }
    }
}
