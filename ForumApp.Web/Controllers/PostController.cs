using System;
using System.Collections.Generic;
using System.Linq;
using ForumApp.Data;
using ForumApp.Data.Models;
using ForumApp.Web.Models.Post;
using ForumApp.Web.Models.Reply;
using Microsoft.AspNetCore.Mvc;

namespace ForumApp.Web.Controllers
{
    public class PostController : Controller
    {
        private readonly IPost _postService; 
        public PostController(IPost postService)
        {
            _postService = postService;
        }
        public IActionResult Index(int id)
        {
            var post = _postService.GetById(id);

            var replies = BuildPostReplies(post.Replies);

            var model = new PostIndexModel
            {
                Id = post.Id,
                AuthorImageUrl = post.User.ProfileImageUrl,
                AuthorName=post.User.UserName,
                AuthorId = post.User.Id,
                AuthorRating = post.User.Rating,
                Created = post.Created,
                PostContent = post.Content,
                Title = post.Title,
                Replies = replies
            };
            return View(model);
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