using Sat.Recruitment.Common.Enums;

namespace Sat.Recruitment.Common
{
    public interface IUserService
    {
        public decimal CalculateGift(UserType type, decimal money);
    }
}