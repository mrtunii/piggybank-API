using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Database;
using Data.Request.User;
using Data.Response.User;

namespace Core.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserResponse> GetAsync(string username, string password);
        Task<UserResponse> GetAsync(Guid id);
        Task<UserResponse> CreateAsync(UserRequest request);
        Task<UserResponse> UpdateAsync(Guid id, UserRequest request);
        Task<List<UserRatingResponse>> GetRating(Guid loggedUserId);

    }
}