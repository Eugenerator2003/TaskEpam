using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibrary
{
    public class TakenBook : LibraryDBObject
    {
        public int UserId { get; }

        public int BookId { get; }

        public bool IsReturned { get; private set; }

        public TakenBook (int id, int userId, int bookId, bool isReturned)
        {
            Id = id;
            UserId = userId;
            BookId = bookId;
            IsReturned = isReturned;
        }
    }
}
