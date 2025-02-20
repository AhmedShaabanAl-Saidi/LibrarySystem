using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Models
{
    public class Library
    {
        public List<Book> books { get; set; }
        public List<User> users { get; set; }

        public Library() 
        {
            books = new List<Book>();
            users = new List<User>();
        }

        public void AddBook(Book book)
        {
            books.Add(book);
        }

        public void AddUser(User user)
        {
            users.Add(user);
        }

        public void RemoveBook(Book book) 
        {
            books.Remove(book);
        }

        public void RemoveUser(User user)
        {
            users.Remove(user);
        }

        public List<Book> SearchBooks(string criteria)
        {
            return books.FindAll(book => book.Title.Contains(criteria) 
            || book.Author.Contains(criteria) || book.Category.Contains(criteria));
        }

        public List<User> SearchUsers(string criteria)
        {
            return users.FindAll(user => user.Name.Contains(criteria) 
            || user.Id.Contains(criteria));
        }

        public void GenerateReport()
        {
            Console.WriteLine("********** Library Report **********");

            Console.WriteLine("***** All Books *****");

            books.ForEach(book => Console.WriteLine($"Title: {book.Title} Author: {book.Author} Category: {book.Category} ISBN: {book.ISBN}"));

            Console.WriteLine("***** All Users *****");

            users.ForEach(user => Console.WriteLine($"Name: {user.Name} ID: {user.Id}"));

            Console.WriteLine("***** Borrowed Books *****");

            foreach (var user in users)
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
