using Sat.Recruitment.Api.Models;

namespace Sat.Recruitment.Common;

public interface IUserRepository
{
    public void Add(User newUser);
    public Task<List<User>> GetAllUsers();
    public Task SaveChanges();
}