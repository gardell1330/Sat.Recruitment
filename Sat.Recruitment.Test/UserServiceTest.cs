using Sat.Recruitment.Common.Enums;
using Sat.Recruitment.Common.Service;
using Xunit;

namespace Sat.Recruitment.Test
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class UserServiceTest
    {
        [Fact]
        public void calculategift_premium_giftdouble()
        {
            var service = new UserService();
            var result = service.CalculateGift(UserType.Premium, 101);
            Assert.Equal(303, result);
        }
        
        [Fact]
        public void calculategift_premium_withoutgift()
        {
            var service = new UserService();
            var result = service.CalculateGift(UserType.Premium, 100);
            Assert.Equal(100, result);
        }
        
        [Fact]
        public void calculategift_superUser_withoutgift()
        {
            var service = new UserService();
            var result = service.CalculateGift(UserType.SuperUser, 100);
            Assert.Equal(100, result);
        }
        
        [Fact]
        public void calculategift_superUser_withgift()
        {
            var service = new UserService();
            var result = service.CalculateGift(UserType.SuperUser, 101);
            Assert.Equal((decimal)121.2, result);
        }
        
        [Fact]
        public void calculategift_normal_withoutgift()
        {
            var service = new UserService();
            var result = service.CalculateGift(UserType.Normal, 100);
            Assert.Equal(100, result);
        }
        
        [Fact]
        public void calculategift_normal_withgift()
        {
            var service = new UserService();
            var result = service.CalculateGift(UserType.Normal, 101);
            Assert.Equal((decimal)113.12, result);
        }
    }
}