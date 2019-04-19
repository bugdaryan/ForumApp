using ForumApp.Data;
using ForumApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumApp.Service
{
    public class PostService : IPost
    {
        private readonly ApplicationDbContext _context;
        public PostService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Post post)
        {
            await _context.AddAsync(post);
            await _context.SaveChangesAsync();
        }

        public async Task AddReplyAsync(PostReply reply)
        {
            await _context.PostReplies.AddAsync(reply);
            await _context.SaveChangesAsync();
        }

        public Task ArchiveAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(int id)
        {
            var post = GetById(id);
            _context.Remove(post);
            await _context.SaveChangesAsync();
        }

        public async Task EditPostContentAsync(int id, string newContent)
        {
            var post = GetById(id);
            post.Content = newContent;
            _context.Update(post);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Post> GetAll()
        {
            return _context.Posts
                .Include(post => post.Forum)
                .Include(post => post.User)
                .Include(post => post.Replies).ThenInclude(reply => reply.User);
        }

        public IEnumerable<ApplicationUser> GetAllUsers(IEnumerable<Post> posts)
        {
            var users = new HashSet<ApplicationUser>();
            foreach(var post in posts)
            {
                users.Add(post.User);
            }
            return users;
        }

        public Post GetById(int id)
        {
            return GetAll().First(post => post.Id == id);
        }

        public IEnumerable<Post> GetFilteredPosts(Forum forum, string searchQuery)
        {
            string query = System.Text.RegularExpressions.Regex.Replace(searchQuery, @"\s+", " ").Trim().ToLower();
            return forum.Posts
                .Where(post =>
                post.Title.ToLower().Contains(query)
                || post.Content.ToLower().Contains(query));
        }

        public string GetForumImageUrl(int id)
        {
            return GetById(id).Forum.ImageUrl;
        }

        public IEnumerable<Post> GetLatestPosts(int count)
        {
            var allPosts = GetAll().OrderByDescending(post => post.Created);
            return allPosts.Take(count);
        }

        public IEnumerable<Post> GetPostsBetween(DateTime start, DateTime end)
        {
            return GetAll().Where(post => post.Created > start && post.Created < end); 
        }

        public IEnumerable<Post> GetPostsByForumId(int id)
        {
            return _context.Forums.First(forum => forum.Id == id).Posts;
        }

        public IEnumerable<Post> GetPostsByUserId(int id)
        {
            return GetAll().Where(post => post.User.Id == id.ToString());
        }

        public int GetReplyCount(int id)
        {
            return GetById(id).Replies.Count();
        }
    }
}
