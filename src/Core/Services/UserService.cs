using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Helpers;
using Core.Services.Interfaces;
using Data.Database;
using Data.Request.User;
using Data.Response.User;
using Microsoft.EntityFrameworkCore;

namespace Core.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<UserResponse> GetAsync(string username, string password)
        {
            var existingUser =
                await _context.Users.FirstOrDefaultAsync(c => c.Username == username && !c.DateDeleted.HasValue);
            if (existingUser == null) throw new Exception("მომხმარებლის სახელი ან პაროლი არასწორია");

            if (!Cryptography.Validate(password, existingUser.PasswordSalt, existingUser.Password))
                throw new Exception("მომხმარებლის სახელი ან პაროლი არასწორია");

            return new UserResponse
            {
                Id = existingUser.Id,
                Firstname = existingUser.Firstname,
                Lastname = existingUser.Lastname,
                PhoneNumber = existingUser.PhoneNumber,
                Username = existingUser.Username
            };
        }

        public async Task<UserResponse> GetAsync(Guid id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(c => c.Id == id && !c.DateDeleted.HasValue);
            if (user == null) throw new Exception("ასეთი მომხმარებელი არ არსებობს");

            return new UserResponse
            {
                Id = user.Id,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                PhoneNumber = user.PhoneNumber,
                Username = user.Username
            };
        }

        public async Task<UserResponse> CreateAsync(UserRequest request)
        {
            if (string.IsNullOrEmpty(request.Username)) throw new Exception("მომხმარებლის სახელი ცარიელია");
            if (string.IsNullOrEmpty(request.Password)) throw new Exception("პაროლი ცარიელია");
            var existingUser = await _context.Users.FirstOrDefaultAsync(c =>
                (c.Username == request.Username || c.PhoneNumber == request.PhoneNumber) && !c.DateDeleted.HasValue);
            if (existingUser != null) throw new Exception("ასეთი მომხმარებელი უკვე არსებობს");

            var newUser = new User
            {
                Id = Guid.NewGuid(),
                Firstname = request.Firstname,
                Lastname = request.Lastname,
                PhoneNumber = request.PhoneNumber,
                PasswordSalt = Cryptography.CreateSalt(),
                Username = request.Username
            };
            newUser.Password = Cryptography.CreateHash(request.Password, newUser.PasswordSalt);

            await _context.Users.AddAsync(newUser);

            await _context.SaveChangesAsync();

            return new UserResponse
            {
                Id = newUser.Id,
                Firstname = newUser.Firstname,
                Lastname = newUser.Lastname,
                PhoneNumber = newUser.PhoneNumber,
                Username = newUser.Username
            };
        }

        public async Task<UserResponse> UpdateAsync(Guid id, UserRequest request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(c => c.Id == id && !c.DateDeleted.HasValue);
            if (user == null) throw new Exception("ასეთი მომხმარებელი არ არსებობს");

            var userWithSamePhone = await _context.Users.FirstOrDefaultAsync(c =>
                c.Id != id && c.PhoneNumber == request.PhoneNumber &&
                !c.DateDeleted.HasValue);
            if (userWithSamePhone != null)
                throw new Exception("მომხმარებელი ასეთი ტელეფონის ნომრით უკვე არსებობს");

            user.Firstname = request.Firstname;
            user.Lastname = request.Lastname;
            user.PhoneNumber = request.PhoneNumber;

            await _context.SaveChangesAsync();

            return new UserResponse
            {
                Id = user.Id,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                PhoneNumber = user.PhoneNumber,
                Username = user.Username
            };
        }

        public async Task<List<UserRatingResponse>> GetRating(Guid loggedUserId)
        {
            var users = await _context.Users.OrderByDescending(c => c.Point).Where(c => !c.DateDeleted.HasValue)
                .ToListAsync();
            var result = new List<UserRatingResponse>();
            foreach (var user in users)
            {
                var fullName = $"{user.Firstname} {user.Lastname}";
                if (user.Id == loggedUserId)
                {
                    result.Add(new UserRatingResponse
                    {
                        FullName = fullName.SubsituteString(1, fullName.Length - 2, new string('*',fullName.Length -2)),
                        Points = user.Point,
                        IsCurrentUser = false
                    });
                }
                else
                {
                    result.Add(new UserRatingResponse
                    {
                        FullName = fullName,
                        Points = user.Point,
                        IsCurrentUser = true
                    });
                }
            }

            return result;
        }
    }
}