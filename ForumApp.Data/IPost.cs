using ForumApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ForumApp.Data
{
    public interface IPost
    {
        Task AddAsync(Post post);
        Task ArchiveAsync(int id);
        Task DeleteAsync(int id);
        Task EditPostContentAsync(int id, string content);

        Task AddReplyAsync(PostReply reply);

        int GetReplyCount(int id);

        Post GetById(int id);
        IEnumerable<Post> GetAll();
        IEnumerable<Post> GetPostsByUserId(int id);
        IEnumerable<Post> GetPostsByForumId(int id);
        IEnumerable<Post> GetPostsBetween(DateTime start, DateTime end);
        IEnumerable<Post> GetFilteredPosts(Forum forum, string searchQuery);
        IEnumerable<ApplicationUser> GetAllUsers(IEnumerable<Post> posts);
        IEnumerable<Post> GetLatestPosts(int count);
        string GetForumImageUrl(int id);
    }
}
