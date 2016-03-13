using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using TangentIdeas.Web.Data.Database;
using TangentIdeas.Web.Data.Models;

namespace TangentIdeas.Web.Data.Services {
    public class UserService : IService<User> {
        private DbConnection _connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        private BlogDatabase GetDatabase() {
            return BlogDatabase.Init(_connection,100);
        }
        public List<User> GetAll() {
            throw new NotImplementedException();
        }

        public List<User> GetWhere(Func<User,bool> predicate) {
            var db = GetDatabase();
            return db.Users.All().Where(predicate).ToList();
        }

        public User GetFirstOrDefault(Func<User,bool> predicate) {
            throw new NotImplementedException();
        }

        public User GetFirstOrDefault(int id) {
            throw new NotImplementedException();
        }

        public int SaveOrUpdate(User entity) {
            throw new NotImplementedException();
        }

        public int Delete(User entity) {
            throw new NotImplementedException();
        }

        public int Delete(int id) {
            throw new NotImplementedException();
        }
    }
}
