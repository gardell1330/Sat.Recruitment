using AutoMapper;
using Sat.Recruitment.Api.Models;

namespace Sat.Recruitment.Api.Profiles
{
    public class DecimalResolver : IValueResolver<UserRequestModel, User, decimal>
    {
        public decimal Resolve(UserRequestModel source, User dest, decimal destMember, ResolutionContext context)
        {
            return decimal.TryParse(source.Money, out var result) ? result : default;
        }
    }
}