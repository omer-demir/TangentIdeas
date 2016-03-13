using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using TangentIdeas.Web.Data.Database;
using TangentIdeas.Web.Data.Models;

namespace TangentIdeas.Web.Data.Services {
    public class BlogService : IService<BlogPost> {
        private DbConnection _connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

        private BlogDatabase GetDatabase() {
            return BlogDatabase.Init(_connection,100);
        }

        public List<BlogPost> GetAll() {
            var db = GetDatabase();
            return db.BlogPosts.All().ToList();
        }

        public List<BlogPost> GetWhere(Func<BlogPost,bool> predicate) {
            var db = GetDatabase();
            return db.BlogPosts.All().Where(predicate).ToList();
        }

        public BlogPost GetFirstOrDefault(Func<BlogPost,bool> predicate) {
            var db = GetDatabase();                       
            return db.BlogPosts.All().FirstOrDefault(predicate);
        }

        public BlogPost GetFirstOrDefault(int id) {
            var db = GetDatabase();
            BlogPost blg = db.BlogPosts.Get(id);
            List<Comment> comments = db.Query<Comment>("SELECT * FROM Comments WHERE BlogPostId=" + id).ToList<Comment>();
            blg.Comments = comments;
            return blg;
        }

        public int SaveOrUpdate(BlogPost entity) {
            var db = GetDatabase();
            if(db.BlogPosts.Get(entity.Id) != null) {
                //update
                return db.BlogPosts.Update(entity.Id,entity);
            }
            int activestatus = entity.ActiveStatus ? 1 : 0;

            var sql = "INSERT INTO [dbo].[BlogPosts]" +
                      "([Title],[Author],[Tags],[ShortDescription],[Content],[ImageUrl],[CreateDate],[ActiveStatus])" +
                      " VALUES(" +
                      "'"+entity.Title+"','"+entity.Author+"','"+entity.Tags+"','"+entity.ShortDescription+"'," +
                      "'" + entity.Content + "','" + entity.ImageUrl + "'," + entity.CreateDate + "," + activestatus + ")";

            return db.Execute(sql);
        }

        public int Delete(BlogPost entity) {
            var db = GetDatabase();
            return db.BlogPosts.Delete(entity.Id) ? 1 : 0;
        }

        public int Delete(int id) {
            var db = GetDatabase();
            return db.BlogPosts.Delete(id) ? 1 : 0;
        }
    }
}
