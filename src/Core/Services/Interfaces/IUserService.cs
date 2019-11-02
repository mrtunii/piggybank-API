using System;
using System.Threading.Tasks;
using Data.Database;
using Data.Request.User;
using Data.Response.User;

namespace Core.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserResponse> GetAsync(string username, string password);
        Task<UserResponse> CreateAsync(UserRequest request);
        Task<UserResponse> UpdateAsync(Guid id, UserRequest request);

    }
}