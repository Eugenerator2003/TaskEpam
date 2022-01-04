using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookLibrary;
using System.Data.SqlClient;
using System.Reflection;

namespace DaoDBLibrary.CRUD
{
    public class MyCRUD<T> : IMyCrud<T> where T : LibraryDBObject
    {
        private SqlConnection _sqlConnection;
        private List<PropertyInfo> _properties;

        public MyCRUD(SqlConnection sqlConnection)
        {
            _sqlConnection = sqlConnection;
            _properties = typeof(T).GetProperties().ToList();
        }

        public void Create(T obj)
        {
            StringBuilder sqlCommandText = new StringBuilder();
            sqlCommandText.Append($"SET IDENTITY_INSERT [{typeof(T).Name}] ON; INSERT INTO [{typeof(T).Name}] (");

            List <PropertyInfo> propertyInfos = _properties.Where(property => !property.PropertyType.IsClass || property.PropertyType == typeof(string)).ToList();

            sqlCommandText.Append(string.Join(",", propertyInfos.Select(property => $"{property.Name}")));
            sqlCommandText.Append(") VALUES (");
            sqlCommandText.Append(string.Join(",", propertyInfos.Select(property => $"{property.Name}")));
            sqlCommandText.Append(");");
            sqlCommandText.Append($"SET IDENTITY_INSERT [{typeof(T).Name}] OFF;");

            SqlCommand sqlCommand = new SqlCommand(sqlCommandText.ToString(), _sqlConnection);

            foreach (PropertyInfo property in propertyInfos)
            {
                sqlCommand.Parameters.AddWithValue($"@{property.Name}", $"{property.GetValue(obj)}");
            }

            _sqlConnection.Open();
            sqlCommand.ExecuteNonQuery();
            _sqlConnection.Close();


        }

        public T Read(int id)
        {
            _sqlConnection.Open();
            object obj = null;

            StringBuilder sqlCommandText = new StringBuilder($"SELECT * FROM [{typeof(T).Name}] WHERE Id = @Id;");
            SqlCommand sqlCommand = new SqlCommand(sqlCommandText.ToString(), _sqlConnection);

            sqlCommand.Parameters.AddWithValue("@Id", $"{id}");

            SqlDataReader dataReader = sqlCommand.ExecuteReader();

            int fieldCount = dataReader.FieldCount;

            if (dataReader.HasRows)
            {
                dataReader.Read();
                obj = LibraryDBObject.CreateObject<T>();

                for(int i = 0; i < fieldCount; i++)
                {
                    string fieldName = dataReader.GetName(i);
                    PropertyInfo propertyInfo = typeof(T).GetProperty(fieldName);
                    propertyInfo?.SetValue(obj, dataReader.GetValue(i));
                }
            }

            dataReader.Close();
            _sqlConnection.Close();
            return (T)obj;
        }

        public void Update(int id, T obj)
        {
            StringBuilder sqlCommandText = new StringBuilder($"UPDATE [{typeof(T).Name}] SET ");

            List<PropertyInfo> propertyInfos = _properties.Where(property => 
                        (!property.PropertyType.IsClass || (property.PropertyType == typeof(string))) && 
                        (property.Name != nameof(LibraryDBObject.Id))
                        ).ToList();

            sqlCommandText.Append(string.Join(",", propertyInfos.Where(property => property.Name != nameof(LibraryDBObject.Id))
                                                               .Select(property => $"[{property.Name}] = @{property.Name}")));

            sqlCommandText.Append($"WHERE [ID] = @{nameof(id)};");

            SqlCommand sqlCommand = new SqlCommand(sqlCommandText.ToString(), _sqlConnection);

            foreach(PropertyInfo property in propertyInfos)
            {
                sqlCommand.Parameters.AddWithValue($"@{property.Name}", $"{property.GetValue(obj)}");
            }
            sqlCommand.Parameters.AddWithValue($"@{nameof(id)}", $"{id}");

            _sqlConnection.Open();
            sqlCommand.ExecuteNonQuery();
            _sqlConnection.Close();
        }

        public void Delete(int id)
        {
            string sqlCommandText = $"DELETE FROM [{typeof(T).Name}] WHERE ID = @ID;";
            SqlCommand sqlCommand = new SqlCommand(sqlCommandText, _sqlConnection);
            sqlCommand.Parameters.AddWithValue("@ID", $"{id}");
            _sqlConnection.Open();
            sqlCommand.ExecuteNonQuery();
            _sqlConnection.Close();
        }
    }
}
