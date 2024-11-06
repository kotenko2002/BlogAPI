namespace BlogAPI.PL.Models.Posts
{
    public record PostResponse(int Id, string Title, string Description, UserResponse Author);
    public record UserResponse(int Id, string FirstName, string LastName);
}
