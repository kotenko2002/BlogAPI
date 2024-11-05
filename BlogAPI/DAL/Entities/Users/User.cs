using BlogAPI.DAL.Entities.Posts;
using Microsoft.AspNetCore.Identity;

namespace BlogAPI.DAL.Entities.Users
{
    public class User : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}
