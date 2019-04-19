using Microsoft.AspNetCore.Http;
using System;

namespace ForumApp.Web.Models.Profile
{
    public class ProfileModel
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public int UserRating { get; set; }
        public string ProfileImageUrl { get; set; }

        public DateTime MemberSince { get; set; }
        public IFormFile MyProperty { get; set; }
    }
}
