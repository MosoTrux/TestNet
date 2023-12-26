using Dapper;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using TestNet.Core.Entities;
using TestNet.Core.Repositories;

namespace TestNet.Infrastructure.Repositories
{
    public class DapperRepository<T> : IRepository<T> where T : BaseEntity
    {
        public IDbTransaction Transaction { get; private set; }
        public IDbConnection Connection { get { return Transaction.Connection; } }

        public DapperRepository(IDbTransaction transaction)
        {
            Transaction = transaction;
        }

        public void Add(T entity, Dictionary<string, object> parameteres)
        {
            var query = GetQueryInsert(parameteres);
            entity.Id = Connection.ExecuteScalar<long>(
                query,
                param: GetParams(parameteres),
                transaction: Transaction
                );
        }

        public T GetById(long id)
        {
            var query = $"SELECT * FROM {typeof(T).Name} Where Id = @id ";
            return Connection.Query<T>(query, param: new { Id = id }, transaction: Transaction).FirstOrDefault();
        }

        public void Update(Dictionary<string, object> parameteres)
        {
            var query = GetQueryUpdate(parameteres);
            Connection.Execute(
                query,
                param: GetParams(parameteres, true),
                transaction: Transaction
                );
        }

        private string GetQueryInsert(Dictionary<string, object> dictionary)
        {
            string columns = "(";
            string values = "Values(";
            foreach (var kvp in dictionary)
            {
                columns += $"{kvp.Key}, ";
                values += $"@{kvp.Key}, ";
            }
            columns = $"{columns[..^2]})";
            values = $"{values[..^2]})";

            return $"INSERT INTO {typeof(T).Name}{columns} {values}; SELECT SCOPE_IDENTITY()";
        }

        private string GetQueryUpdate(Dictionary<string, object> dictionary)
        {
            string seters = string.Empty;
            foreach (var kvp in dictionary)
            {
                if (kvp.Key.ToUpper().Equals("ID")) continue;
                seters += $"{kvp.Key} = @{kvp.Key}, ";
            }
            seters = $"{seters[..^2]}";

            return $"UPDATE {typeof(T).Name} SET {seters} WHERE Id = @Id";
        }
        private ExpandoObject GetParams(Dictionary<string, object> dictionary, bool addId = false)
        {
            var expandoObject = new ExpandoObject() as IDictionary<string, object>;

            foreach (var kvp in dictionary)
            {
                if (kvp.Key.ToUpper().Equals("ID")) continue;
                expandoObject.Add(kvp.Key, kvp.Value);
            }
            if(addId) expandoObject.Add("Id", dictionary["Id"]);

            return (ExpandoObject)expandoObject;
        }
    }
}
