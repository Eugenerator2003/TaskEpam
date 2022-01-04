using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibrary
{
    public class Book : LibraryDBObject
    {
        public enum BookGenre
        {

        }

        public enum BookCondition
        {

        }

        public string Name { get; }

        public string Author { get; }

        public BookGenre Genre { get; }

        public BookCondition Condition { get; }

        public Book(int id, string name, string author, BookGenre genre, BookCondition condition)
        {
            Id = id;
            Name = name;
            Author = author;
            Genre = genre;
            Condition = condition;
        }
    }
}
