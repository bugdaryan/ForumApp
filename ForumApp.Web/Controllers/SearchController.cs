using ForumApp.Data;
using ForumApp.Data.Models;
using ForumApp.Web.Models.Forum;
using ForumApp.Web.Models.Post;
using ForumApp.Web.Models.Search;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ForumApp.Web.Controllers
{
    public class SearchController : Controller
    {
        private readonly IPost _postService;

        public SearchController(IPost postService)
        {
            _postService = postService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Result(string searchQuery)
        {
            var posts = _postService.GetFilteredPosts(searchQuery);

            var noPostsFound = (!string.IsNullOrEmpty(searchQuery) && !posts.Any());

            var postListingModel = posts.Select(post => new PostListingModel
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

            var model = new SearchResultModel
            {
                Posts = postListingModel,
                SearchQuery = searchQuery,
                EmptySearchResult = noPostsFound
            };

            return View(model);
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
            return RedirectToAction("Results", new { searchQuery });
        }
    }
}