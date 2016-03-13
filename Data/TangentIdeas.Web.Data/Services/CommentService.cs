using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using TangentIdeas.Web.Data.Database;
using TangentIdeas.Web.Data.Models;

namespace TangentIdeas.Web.Data.Services
{

    public class CommentService : IService<Comment>
    {
        private DbConnection _connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

        private BlogDatabase GetDatabase()
        {
            return BlogDatabase.Init(_connection, 100);
        }
        public List<Comment> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<Comment> GetWhere(Func<Comment, bool> predicate)
        {
            var db = GetDatabase();
            return db.Comments.All().Where(predicate).ToList();
        }

        public Comment GetFirstOrDefault(Func<Comment, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public Comment GetFirstOrDefault(int id)
        {
            throw new NotImplementedException();
        }

        public int SaveOrUpdate(Comment entity)
        {
            var db = GetDatabase();
            if (db.Comments.Get(entity.Id) != null)
            {
                //update
                return db.Comments.Update(entity.Id, entity);
            }
            int activestatus = entity.ActiveStatus ? 1 : 0;
            var sql="INSERT INTO [dbo].[Comments]" +
                    "([BlogPostId],[Name],[Email],[Website],[CommentText],[CreateDate],[ActiveStatus]) " +
                    "VALUES(" + entity.BlogPostId + ",'" + entity.Name + "','" + entity.Email + "'," +
                    "'" + entity.Website + "','" + entity.CommentText + "'," + entity.CreateDate + "," + activestatus + ">)";

            return db.Comments.Insert(entity) ?? 0;
        }

        public int Delete(Comment entity)
        {
            throw new NotImplementedException();
        }

        public int Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Comment> GetCommentsByBlogId(int bolgId)
        {
            var db = GetDatabase();
            return db.Query<Comment>("SELECT * FROM Comments WHERE BlogPostId=" + bolgId).ToList<Comment>();
        }
    }
}
