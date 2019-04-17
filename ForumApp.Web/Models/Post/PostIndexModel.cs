using ForumApp.Web.Models.Reply;
using System;
using System.Collections.Generic;

namespace ForumApp.Web.Models.Post
{
    public class PostIndexModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string AuthorId { get; set; }
        public string AuthorName { get; set; }
        public string AuthorImageUrl { get; set; }
        public int AuthorRating { get; set; }
        public DateTime Created { get; set; }
        public string PostContent { get; set; }
        public string ForumTitle { get; set; }
        public int ForumId { get; set; }

        public IEnumerable<PostReplyModel> Replies { get; set; }
    }
}
