namespace Data.Response.User
{
    public class UserRatingResponse
    {
        public string FullName { get; set; }
        public int Points { get; set; }
        public bool IsCurrentUser { get; set; }
    }
}