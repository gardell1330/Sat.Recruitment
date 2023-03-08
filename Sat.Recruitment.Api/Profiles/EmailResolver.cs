using System;
using AutoMapper;
using Sat.Recruitment.Api.Models;

namespace Sat.Recruitment.Api.Profiles
{
    public class EmailResolver : IValueResolver<UserRequestModel, User, string>
    {
        public string Resolve(UserRequestModel source, User dest, string destMember, ResolutionContext context)
        {
            var aux = source.Email.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);

            var atIndex = aux[0].IndexOf("+", StringComparison.Ordinal);

            aux[0] = atIndex < 0 ? aux[0].Replace(".", "") : aux[0].Replace(".", "").Remove(atIndex);

            return string.Join("@", new string[] { aux[0], aux[1] });
        }
    }
}