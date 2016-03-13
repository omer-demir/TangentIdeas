using System.Web;
using System.Web.Mvc;

namespace TangentIdeas.Web.Blog.Areas.Admin.Models {
    public class BlogPostModel {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Tags { get; set; }
        public string ShortDescription { get; set; }
        [AllowHtml]
        public string Content { get; set; }
        public HttpPostedFileBase ImageUrl { get; set; }
    }
}