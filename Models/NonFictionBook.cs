using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Models
{
    class NonFictionBook : Book
    {
        public string? Subject { get; set; }
        public int Edition { get; set; }
        public NonFictionBook(string title, string author, string category, string isbn, string? subject, int edition) : base(title, author, category, isbn)
        {
            Subject = subject;
            Edition = edition;
        }
    }
}
