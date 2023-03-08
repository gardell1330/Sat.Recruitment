using Sat.Recruitment.Common.Enums;

namespace Sat.Recruitment.Common.Service
{
    public class UserService : IUserService
    {
        public decimal CalculateGift(UserType type, decimal money)
        {
            decimal gif;
            decimal percentage;
            
            switch (type)
            {
                case UserType.Normal:
                    switch (money)
                    {
                        case > 100:
                        {
                            percentage = Convert.ToDecimal(0.12);
                            //If new user is normal and has more than USD100
                            gif = money * percentage;
                            return money + gif;
                        }
                        case < 100 and > 10:
                        {
                            percentage = Convert.ToDecimal(0.8);
                            gif = money * percentage;
                            return money + gif;
                        }
                    }
                    break;
                case UserType.SuperUser when money > 100:
                {
                    percentage = Convert.ToDecimal(0.20);
                    gif = money * percentage;
                    return money + gif;
                }

                case UserType.Premium when money > 100:
                {
                    gif = money * 2;
                    return money + gif;   
                }

                default:
                    return money;
            }
            
            return money;
        }
    }
}