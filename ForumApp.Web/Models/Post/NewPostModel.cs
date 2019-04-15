using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumApp.Web.Models.Post
{
    public class NewPostModel
    {
        public string ForumTitle { get; set; }
        public int ForumId { get; set; }
        public string AuthorName { get; set; }
        public string ForumImageUrl { get; set; }
        public string Content { get; set; }
        public string Title { get; set; }
    }
}
