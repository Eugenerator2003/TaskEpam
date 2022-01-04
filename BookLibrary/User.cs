using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibrary
{
    public class User : LibraryDBObject
    {
        public enum UserSex
        {
            Male,
            Female
        }

        public string Surname { get; }

        public string Name { get; }

        public UserSex Sex { get; }

        public DateTime BirthdayDate { get; }

        public User(int id, string surname, string name, UserSex sex, DateTime birthdayDate)
        {
            Id = id;
            Surname = surname;
            Name = name;
            Sex = sex;
            BirthdayDate = birthdayDate;
        }
        
    }
}
