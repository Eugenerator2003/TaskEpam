using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DaoDBLibrary.CRUD;
using BookLibrary;
using System.Data.SqlClient;
using System.Reflection;

namespace DaoDBLibrary.ORM
{
    public class MyDB<T> where T : LibraryDBObject
    {
        private List<T> _objects;
        private MyCRUD<T> _crud;
        private SqlConnection _sqlConnection;

        public MyDB(SqlConnection sqlConnection)
        {
            _sqlConnection = sqlConnection;
            _crud = new MyCRUD<T>(_sqlConnection);
            _objects = _GetDataTable().ToList();
        }

        public void Add(T obj)
        {
            _crud.Create(obj);
            _objects.Add(obj);
        }

        public T Read(int id) => _objects.FirstOrDefault(item => item.Id == id);

        public void Update(int id, T obj)
        {
            _crud.Update(id, obj);
            T objFromList = _objects.FirstOrDefault(item => item.Id == id);
            objFromList = obj;
        }

        public void Delete(int id)
        {
            _crud.Delete(id);
            _objects.Remove(_objects.FirstOrDefault(item => item.Id == id));
        }


        private List<T> _GetDataTable()
        {
            _sqlConnection.Open();

            string sqlCommandText = $"SELECT * FROM [{typeof(T).Name}]";
            SqlCommand sqlCommand = new SqlCommand(sqlCommandText, _sqlConnection);
            SqlDataReader dataReader = sqlCommand.ExecuteReader();

            List<T> list = new List<T>();
            LibraryDBObject obj = LibraryDBObject.CreateObject<T>();

            int columns = dataReader.FieldCount;
            if (dataReader.HasRows)
            {
                while(dataReader.Read())
                {
                    for(int i = 0; i < columns; i++)
                    {
                        string name = dataReader.GetName(i);
                        PropertyInfo propertyInfo = obj.GetType().GetProperty(name);
                        if(!(dataReader.GetValue(i) != DBNull.Value))
                        {
                            propertyInfo?.SetValue(obj, dataReader.GetValue(i));
                        }
                    }
                    list.Add((T)obj);
                    obj = LibraryDBObject.CreateObject(typeof(T).FullName);
                }
            }

            _sqlConnection.Close();
            return list;
        }
    }
}
