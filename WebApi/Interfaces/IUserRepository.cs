using System;

namespace WebApi.Interfaces
{
    public interface IUserRepository : IGenericRepository <User>
    {
        List<User> GetAllInformation(string info);
    }
}