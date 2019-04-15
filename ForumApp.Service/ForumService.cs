using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ForumApp.Data;
using ForumApp.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ForumApp.Service
{
    public class ForumServices : IForum
    {
        private readonly ApplicationDbContext _context;
        public ForumServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task Create(Forum forum)
        {
            throw new System.NotImplementedException();
        }

        public Task Delete(int forumId)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Forum> GetAll()
        {
            return _context.Forums.Include(forum => forum.Posts).AsNoTracking(); ;
        }

        public IEnumerable<ApplicationUser> GetAllActiveUsers()
        {
            throw new System.NotImplementedException();
        }

        public Forum GetById(int id)
        {
            return _context.Forums.Where(f => f.Id == id)
                .Include(f => f.Posts).ThenInclude(p => p.User)
                .Include(f => f.Posts).ThenInclude(p => p.Replies).ThenInclude(r => r.User)
                .FirstOrDefault();
        }

        public Task UpdateForumDescription(int forumId, string newDescription)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateForumTitle(int forumId, string newTitle)
        {
            throw new System.NotImplementedException();
        }
    }
}
