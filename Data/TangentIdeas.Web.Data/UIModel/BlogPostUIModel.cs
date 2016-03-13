using System.Collections.Generic;
using System.Globalization;
using TangentIdeas.Web.Data.Helpers;
using TangentIdeas.Web.Data.Models;

namespace TangentIdeas.Web.Data.UIModel {
    public class BlogPostUIModel : BaseUIModel {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Tags { get; set; }
        public string ShortDescription { get; set; }
        public string Content { get; set; }
        public string ImageUrl { get; set; }

        public List<CommentUIModel> Comments { get; set; }

        public static BlogPostUIModel MapFromEntity(BlogPost blogPostEntity) {
            List<CommentUIModel> cum = new List<CommentUIModel>();
            if (blogPostEntity.Comments!= null && blogPostEntity.Comments.Count > 0)
            {
                foreach (var item in blogPostEntity.Comments)
                {
                    CommentUIModel c = new CommentUIModel
                    {
                        BlogPostId = item.BlogPostId,
                        CommentText = item.CommentText,
                        CreateDate = item.CreateDate.ToString(CultureInfo.InvariantCulture),
                        Email = item.Email,
                        Id = item.Id,
                        Name = item.Name,
                        ParsedCreateDate = item.CreateDate.ToString(CultureInfo.InvariantCulture).ConvertStringToDate(),
                        Website = item.Website
                    };
                    cum.Add(c);
                }
            }
            return new BlogPostUIModel {
                Author = blogPostEntity.Author,
                Content = blogPostEntity.Content,
                CreateDate = blogPostEntity.CreateDate.ToString(CultureInfo.InvariantCulture),
                ImageUrl = blogPostEntity.ImageUrl,
                Tags = blogPostEntity.Tags,
                Title = blogPostEntity.Title,
                ActiveStatus = blogPostEntity.ActiveStatus,
                ShortDescription = blogPostEntity.ShortDescription,
                Id = blogPostEntity.Id,
                ParsedCreateDate = blogPostEntity.CreateDate.ToString(CultureInfo.InvariantCulture).ConvertStringToDate(),
                Comments = cum
            };
        }
    }
}
