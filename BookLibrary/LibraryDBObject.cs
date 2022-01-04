using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BookLibrary
{
    public abstract class LibraryDBObject
    {
        public int Id { get; protected set; }

        public static LibraryDBObject CreateObject<T>() where T : LibraryDBObject
        {
            return Activator.CreateInstance<T>();
        }

        public static LibraryDBObject CreateObject(string str)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            string type = assembly.GetType(str).ToString();
            return (LibraryDBObject)Activator.CreateInstance(assembly.Location, type).Unwrap();
        }


    }
}
