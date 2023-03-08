using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Sat.Recruitment.Api.Models;
using Sat.Recruitment.Api.Utilities;
using Sat.Recruitment.Common;

namespace Sat.Recruitment.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserService _service;
        private readonly IUserRepository _repository;
        
        public UsersController(
            IMapper mapper, 
            IUserService service,
            IUserRepository repository)
        {
            _mapper = mapper;
            _service = service;
            _repository = repository;
        }

        [HttpPost]
        [Route("/create-user")]
        public async Task<Result> CreateUser(UserRequestModel userRequest)
        {
            var newUser = _mapper.Map<User>(userRequest);
            newUser.Money = _service.CalculateGift(newUser.UserType, newUser.Money);
            _repository.Add(newUser);

            var result = new Result();
            try
            {
                await _repository.SaveChanges();
                result.IsSuccess = true;
                result.Response = "User Created";
            }
            catch (Exception e)
            {
                result.Response = e.Message;
            }

            return result;
        }
    }
}
