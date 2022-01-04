using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using BookLibrary;
using System.Data.SqlClient;

namespace DaoDBLibrary.ORM
{
    public class MyDAO
    {
        private static MyDAO _instance;
        private SqlConnection _sqlConnection;

        public MyDB<Book> Books { get; set; }
        public MyDB<TakenBook> TakenBooks { get; set; }
        public MyDB<User> Users { get; set; }

        private MyDAO()
        {
            _sqlConnection = new SqlConnection("Server=localhost;Database=master;Trusted_Connection=True");
            Books = new MyDB<Book>(_sqlConnection);
            TakenBooks = new MyDB<TakenBook>(_sqlConnection);
            Users = new MyDB<User>(_sqlConnection);
        }

        public static MyDAO Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new MyDAO();
                }
                return _instance;
            }
        }
    }
}