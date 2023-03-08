using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using Sat.Recruitment.Api.Models;
using Sat.Recruitment.Common;
using Sat.Recruitment.Common.Repository;
using Xunit;

namespace Sat.Recruitment.Test
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class UserRepositoryTest
    {
        private Mock<IMapper> _mockMapper;
        private Mock<IUsersFile> _mockUsersFile;
        private UserRepository _repository;

        [Fact]
        public async Task savechanges_success()
        {
            SetupRepository();
            
            var user = new User
            {
                Name = "Test",
                Address = "Test",
                Phone = "Test"
            };
            
            _repository.Add(user);
            
            await Assert.IsAssignableFrom<Task>(_repository.SaveChanges());
        }
        
        [Fact]
        public async Task savechanges_throwsUserEmpty()
        {
            SetupRepository();
            _repository.Add(new User());
            var exception = await Assert.ThrowsAsync<Exception>(() => _repository.SaveChanges());
            Assert.Equal("User is empty", exception.Message);
        }
        
        [Fact]
        public async Task savechanges_throwsDuplicated()
        {
            SetupRepository();
            var user = new User
            {
                Name = "Test",
                Address = "Test",
                Phone = "Test"
            };
            
            var fakeFileContents = "Test";
            var fakeFileBytes = Encoding.UTF8.GetBytes(fakeFileContents);
            var fakeMemoryStream = new MemoryStream(fakeFileBytes);
            _mockUsersFile.Setup(r => r.ReadUsersFromFile()).Returns(new StreamReader(fakeMemoryStream));
            _mockMapper.Setup(r => r.Map<User>(It.IsAny<string>())).Returns(user);
            
            _repository.Add(user);
            
            var exception = await Assert.ThrowsAsync<Exception>(() => _repository.SaveChanges());
            Assert.Equal("User is duplicated", exception.Message);
        }

        private void SetupRepository()
        {
            _mockMapper = new Mock<IMapper>();
            _mockUsersFile = new Mock<IUsersFile>();
            var fakeFileContents = string.Empty;
            var fakeFileBytes = Encoding.UTF8.GetBytes(fakeFileContents);
            var fakeMemoryStream = new MemoryStream(fakeFileBytes);
            _mockUsersFile.Setup(r => r.ReadUsersFromFile()).Returns(new StreamReader(fakeMemoryStream));
            _mockUsersFile.Setup(r => r.WriterUserFromFile()).Returns(new StreamWriter(fakeMemoryStream));
            
            _repository = new UserRepository(_mockMapper.Object, _mockUsersFile.Object);
        }
    }
}