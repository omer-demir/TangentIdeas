using System;
using System.Collections.Generic;
using TangentIdeas.Web.Data.Models;

namespace TangentIdeas.Web.Data.Services {
    public interface IService<T> where T : BaseEntity {
        List<T> GetAll();
        List<T> GetWhere(Func<T,bool> predicate);
        T GetFirstOrDefault(Func<T,bool> predicate);
        T GetFirstOrDefault(int id);
        int SaveOrUpdate(T entity);
        int Delete(T entity);
        int Delete(int id);
    }
}
