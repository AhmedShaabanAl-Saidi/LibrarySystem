using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Models
{
    public class FictionBook : Book
    {
        public string? Series { get; set; }
        public int? Volume { get; set; }
        public FictionBook(string title, string author, string category, string isbn, string? series, int? volume) : base(title, author, category, isbn)
        {
            Series = series;
            Volume = volume;
        }
    }
}
