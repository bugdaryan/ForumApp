using ForumApp.Data;
using ForumApp.Data.Models;
using ForumApp.Web.Models.Post;
using ForumApp.Web.Models.Reply;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumApp.Web.Controllers
{
    public class PostController : Controller
    {
        private readonly IPost _postService;
        private readonly IForum _forumService;
        private static UserManager<ApplicationUser> _userManager;

        public PostController(IPost postService, IForum forumService, UserManager<ApplicationUser> userManager)
        {
            _postService = postService;
            _forumService = forumService;
            _userManager = userManager;
        }
        public IActionResult Index(int id)
        {
            var post = _postService.GetById(id);

            var replies = BuildPostReplies(post.Replies);

            var model = new PostIndexModel
            {
                Id = post.Id,
                AuthorImageUrl = post.User.ProfileImageUrl,
                AuthorName = post.User.UserName,
                AuthorId = post.User.Id,
                AuthorRating = post.User.Rating,
                Created = post.Created,
                PostContent = post.Content,
                Title = post.Title,
                Replies = replies
            };
            return View(model);
        }

        public IActionResult Create(int id)
        {
            //id is Forum.Id
            var forum = _forumService.GetById(id);

            var model = new NewPostModel
            {
                ForumId = forum.Id,
                ForumTitle = forum.Title,
                ForumImageUrl = forum.ImageUrl,
                AuthorName = User.Identity.Name
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddPostAsync(NewPostModel model)
        {
            var userId = _userManager.GetUserId(User);
            var user = await _userManager.FindByIdAsync(userId);

            var post = BuildPost(model, user);
        }

        private object BuildPost(NewPostModel model, ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        private IEnumerable<PostReplyModel> BuildPostReplies(IEnumerable<PostReply> replies)
        {
            return replies.Select(reply => new PostReplyModel
            {
                ReplyContent = reply.Content,
                Created = reply.Created,
                Id = reply.Id,
                AuthorId = reply.User.Id,
                AuthorImageUrl = reply.User.ProfileImageUrl,
                AuthorName = reply.User.UserName,
                AuthorRating = reply.User.Rating,
                PostId = reply.Post.Id
            });
        }
    }
}