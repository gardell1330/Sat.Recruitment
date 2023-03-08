using System.Diagnostics;
using AutoMapper;
using Sat.Recruitment.Api.Models;
using Sat.Recruitment.Api.Utilities;

namespace Sat.Recruitment.Common.Repository;

public class UserRepository : IUserRepository
{
    private readonly IMapper _mapper;
    private readonly IUsersFile _usersFile;
    private User _newUser;
    
    public UserRepository(IMapper mapper, IUsersFile usersFile)
    {
        _mapper = mapper;
        _usersFile = usersFile;
        _newUser = new User();
    }

    public void Add(User newUser)
    {
        _newUser = newUser;
    }
    
    public async Task SaveChanges()
    {
        if (string.IsNullOrEmpty(_newUser.Name) && string.IsNullOrEmpty(_newUser.Address) &&
            string.IsNullOrEmpty(_newUser.Phone))
        {
            throw new Exception("User is empty");
        }
        
        try
        {
            foreach (var user in await GetAllUsers())
            {
                if (user.Email == _newUser.Email || user.Phone == _newUser.Phone)
                {
                    throw new Exception("User is duplicated");
                }
                else if (user.Name == _newUser.Name)
                {
                    if (user.Address == _newUser.Address)
                    {
                        throw new Exception("User is duplicated");
                    }

                }
            }

            await _usersFile.WriterUserFromFile().WriteLineAsync(_mapper.Map<string>(_newUser));
        }
        catch(SystemException e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<List<User>> GetAllUsers()
    {
        var users = new List<User>();
        var reader = _usersFile.ReadUsersFromFile();

        while (reader.Peek() >= 0)
        {
            var user = _mapper.Map<User>(await reader.ReadLineAsync());
            users.Add(user);
        }
        reader.Close();

        return users;
    }
}