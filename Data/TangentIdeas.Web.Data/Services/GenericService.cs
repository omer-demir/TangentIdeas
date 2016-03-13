using System;
using System.Collections.Generic;
using System.Linq;
using TangentIdeas.Web.Data.Models;

namespace TangentIdeas.Web.Data.Services {
    public class GenericService<T> : IService<T> where T : BaseEntity {
        public List<T> GetAll() {
            throw new NotImplementedException();
        }

        public List<T> GetWhere(Func<T, bool> predicate) {
            throw new NotImplementedException();
        }

        public T GetFirstOrDefault(Func<T, bool> predicate) {
            throw new NotImplementedException();
        }

        public T GetFirstOrDefault(int id) {
            throw new NotImplementedException();
        }

        public int SaveOrUpdate(T entity) {
            throw new NotImplementedException();
        }

        public int Delete(T entity) {
            throw new NotImplementedException();
        }

        public int Delete(int id) {
            throw new NotImplementedException();
        }

        private string GetTableName(Type typeOfT) {
            return typeOfT.Name.ToUpperInvariant();
        }

        private string CreateSqlQuery(SqlOperations operationType, Type typeOfT) {
            var tableName = GetTableName(typeOfT);
            var columns = GetTableColumns(typeOfT);
            var sqlStatement = "";
            switch (operationType) {
                case SqlOperations.Select:
                    var tempColumns = "";
                    columns.ForEach(a => { tempColumns += a + ","; });
                    tempColumns.Remove(tempColumns.Length - 2, 1);
                    sqlStatement += String.Format("SELECT {0} FROM {1}", tempColumns, tableName);
                    break;
                default:
                    sqlStatement = "";
                    break;
            }
            return sqlStatement;
        }

        private List<string> GetTableColumns(Type typeOfT) {
            var columns = new List<string>();
            var tableName = GetTableName(typeOfT);
            typeOfT.GetProperties()
                .ToList()
                .ForEach(a => { columns.Add(String.Format("[{0}].[{1}]", tableName, a.Name)); });
            return columns;
        }

        private enum SqlOperations {
            Select,
            Insert,
            Update,
            Delete
        }
    }
}