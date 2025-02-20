using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Models
{
    public class Book
    {
        public string? Title { get; set; }
        public string? Author { get; set; }
        public string? Category { get; set; }
        public string? ISBN { get; set; }
        public bool IsAvailable { get; set; } = true;

        public Book(string title , string author , string category , string isbn)
        {
            Title = title;
            Author = author;
            Category = category;
            ISBN = isbn;
        }

        public void BorrowBook()
        {
            IsAvailable = false;
        }

        public void ReturnBook()
        {
            IsAvailable = true;
        }
    }
}
