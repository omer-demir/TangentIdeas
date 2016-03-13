using Dapper;
using TangentIdeas.Web.Data.Models;

namespace TangentIdeas.Web.Data.Database {
    public class BlogDatabase : Database<BlogDatabase> {
        public Table<BlogPost> BlogPosts { get; set; }
        public Table<User> Users { get; set; }
        public Table<Comment> Comments { get; set; }
    }
}
