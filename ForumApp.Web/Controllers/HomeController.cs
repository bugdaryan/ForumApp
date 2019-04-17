using ForumApp.Data;
using ForumApp.Data.Models;
using ForumApp.Web.Models.Forum;
using ForumApp.Web.Models.Home;
using ForumApp.Web.Models.Post;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ForumApp.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPost _postService;
        public HomeController(IPost postService)
        {
            _postService = postService;
        }
        public IActionResult Index()
        {
            var model = BuildIndexModel();
            return View(model);
        }

        private HomeIndexModel BuildIndexModel()
        {
            var latest = _postService.GetLatestPosts(10);
            var posts = latest.Select(post => new PostListingModel
            {
                Id = post.Id,
                Title = post.Title,
                AuthorName = post.User.UserName,
                AuthorId = post.User.Id,
                AuthorRating = post.User.Rating,
                DatePosted = post.Created.ToString(),
                RepliesCount = _postService.GetReplyCount(post.Id),
                Forum = BuildForumListing(post.Forum)
            });
            return new HomeIndexModel
            {
                LatestPosts = posts
            };
        }

        private ForumListingModel BuildForumListing(Forum forum)
        {
            return new ForumListingModel
            {
                Title = forum.Title,
                Id = forum.Id,
                Description = forum.Description,
                ImageUrl = forum.ImageUrl
            };
        }

        [HttpPost]
        public IActionResult Search(string searchQuery)
        {
            return RedirectToAction("Topic", "Forum", new { searchQuery });
        }
    }
}
