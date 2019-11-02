using System;

namespace Data.Response.User
{
    public class UserResponse
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string PhoneNumber { get; set; }
    }
}