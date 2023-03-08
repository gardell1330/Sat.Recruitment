using System;
using AutoMapper;
using Sat.Recruitment.Api.Models;
using Sat.Recruitment.Common.Enums;

namespace Sat.Recruitment.Api.Profiles
{
    public class StringUserResolver : IValueResolver<string, User, object>
    {
        public object Resolve(string source, User dest, object destMember, ResolutionContext context)
        {
            return new User
            {
                Name = source.Split(',')[0],
                Email = source.Split(',')[1],
                Phone = source.Split(',')[2],
                Address = source.Split(',')[3],
                UserType = Enum.TryParse<UserType>(source.Split(',')[4], out var resultEnum) ? resultEnum : default,
                Money = decimal.TryParse(source.Split(',')[5], out var resultDecimal)? resultDecimal : default,
            };
        }
    }
}