using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;
using BookLibrary;
using DaoDBLibrary.CRUD;
using DaoDBLibrary.ORM;

namespace DaoDBLibraryTests
{
    [TestClass]
    public class MyDAOTest
    {
        [TestMethod()]
        public void CreatingTest()
        {
            MyDAO dao = MyDAO.Instance;
            var usersDB = dao.Users;
            User userExpected = new User(1, "Surname", "Name", User.UserSex.Male, new DateTime(1999, 12, 31));
            usersDB.Add(userExpected);
            User userResulted = usersDB.Read(1);
            Assert.AreEqual(userExpected, userResulted);
        }
    }
}
