using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Models
{
    public class Library
    {
        public List<Book> Books { get; set; }
        public List<User> Users { get; set; }

        public Library() 
        {
            Books = new List<Book>();
            Users = new List<User>();
        }

        public void AddBook(Book book)
        {
            Books.Add(book);
        }

        public void AddUser(User user)
        {
            Users.Add(user);
        }

        public void RemoveBook(Book book) 
        {
            Books.Remove(book);
        }

        public void RemoveUser(User user)
        {
            Users.Remove(user);
        }

        public List<Book> SearchBooks(string criteria)
        {
            return Books.FindAll(book => book.Title.Contains(criteria) 
            || book.Author.Contains(criteria) || book.Category.Contains(criteria));
        }

        public List<User> SearchUsers(string criteria)
        {
            return Users.FindAll(user => user.Name.Contains(criteria) 
            || user.Id.Contains(criteria));
        }

        public void GenerateReport()
        {
            Console.WriteLine("********** Library Report **********");

            Console.WriteLine("***** All Books *****");

            Books.ForEach(book => Console.WriteLine($"Title: {book.Title} Author: {book.Author} Category: {book.Category} ISBN: {book.ISBN}"));

            Console.WriteLine("***** All Users *****");

            Users.ForEach(user => Console.WriteLine($"Name: {user.Name} ID: {user.Id}"));

            Console.WriteLine("***** Borrowed Books *****");

            foreach (var user in Users)
            {
                if (user.BorrowedBooks.Count > 0)
                {
                    Console.WriteLine($"User: {user.Name} ID: {user.Id}");
                    user.BorrowedBooks.ForEach(book => Console.WriteLine($"Title: {book.Title} Author: {book.Author} Category: {book.Category} ISBN: {book.ISBN}"));
                }
            }

            Console.WriteLine("********** End of Report **********");
        }
    }
}
