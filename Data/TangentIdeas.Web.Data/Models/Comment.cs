namespace TangentIdeas.Web.Data.Models {
    public class Comment:BaseEntity {
        public int BlogPostId { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }
        public string Website { get; set; }
        public string CommentText { get; set; }
    }
}
