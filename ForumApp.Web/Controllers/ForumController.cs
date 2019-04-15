using ForumApp.Data;
using ForumApp.Web.Models.Forum;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ForumApp.Web.Controllers
{
    public class ForumController : Controller
    {
        private readonly IForum _forumService;
        private readonly IPost _postService;

        public ForumController(IForum forumService)
        {
            _forumService = forumService;
        }

        public IActionResult Index()
        {
            var forums = _forumService.GetAll()
                .Select(forum => new ForumListingModel
            {
                    Id = forum.Id,
                    Title = forum.Title,
                    Description = forum.Description
            });
            var model = new ForumIndexModel
            {
                ForumList = forums
            };

            return View(model);
        }

        public IActionResult Topic(int id)
        {
            var forum = _forumService.GetById(id);
            var post = _postService.GetFilteredPosts(id);

            var postListings = 

            var model = new ForumListingModel
            {
                Id = forum.Id,
                Title = forum.Title,
                Description = forum.Description
            };

            return View(model);
        }
    }
}