using LibrarySystem.Models;
using static System.Reflection.Metadata.BlobBuilder;

namespace LibrarySystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Library library = new Library();

            string filePath = "data.json";

            if (File.Exists(filePath))
            {
                FileManager.LoadData(filePath, out List<Book> books, out List<User> users);
                library.Books = books;
                library.Users = users;
            }

            while(true)
            {
                DisplayMenu();

                Console.Write("Enter your choice: ");

                string userChoice = Console.ReadLine();

                HandleUserChoice(userChoice, library, filePath);
            }
        }

        private static void DisplayMenu()
        {
            Console.WriteLine("""
                ********** Library System **********
                1- Manage Books
                2- Manage Users
                3- Borrow/Return Books
                4- Search
                5- Generate Report
                6- Exit
                """);
        }

        private static void HandleUserChoice(string userChoice, Library library, string filePath)
        {
            switch (userChoice)
            {
                case "1":
                    ManageBooks(library);
                    break;
                case "2":
                    ManageUsers(library);
                    break;
                case "3":
                    BorrowReturnBooks(library);
                    break;
                case "4":
                    Search(library);
                    break;
                case "5":
                    library.GenerateReport();
                    break;
                case "6":
                    FileManager.SaveData(filePath, library.Books, library.Users);
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }

        private static void ManageBooks(Library library)
        {
            Console.Clear();

            while (true)
            {
                Console.WriteLine("""
                    ********** Manage Books **********
                    1- Add Book
                    2- Remove Book
                    3- update Book
                    4- List Books
                    5- Back
                    """);

                Console.Write("Enter your choice: ");

                string userChoice = Console.ReadLine();

                switch (userChoice)
                {
                    case "1":
                        AddBook(library);
                        break;
                    case "2":
                        RemoveBook(library);
                        break;
                    case "3":
                        UpdateBook(library);
                        break;
                    case "4":
                        ListBooks(library);
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        private static void AddBook(Library library)
        {
            Console.Clear();

            Console.WriteLine("Enter Book Type [1 For Fiction , 2 For Non-Fiction]");
            string bookType = Console.ReadLine();

            Console.WriteLine("Enter Book Title :");
            string title = Console.ReadLine()!;
            Console.WriteLine("Enter Book Author :");
            string author = Console.ReadLine();
            Console.WriteLine("Enter Book Category :");
            string category = Console.ReadLine();
            Console.WriteLine("Enter Book ISBN :");
            string isbn = Console.ReadLine();

            Book book = bookType == "1" ? AddFictionBook(title, author, category, isbn) : AddNonFictionBook(title, author, category, isbn);

            library.AddBook(book);

            Console.WriteLine("Book added successfully.");
        }

        private static FictionBook AddFictionBook(string title, string author, string category, string isbn)
        {
            Console.Clear();

            Console.WriteLine("Enter Book Series :");
            string series = Console.ReadLine();

            Console.WriteLine("Enter Book Volume :");
            int volume = int.Parse(Console.ReadLine());

            return new FictionBook(title, author, category, isbn, series, volume);
        }

        private static NonFictionBook AddNonFictionBook(string title, string author, string category, string isbn)
        {
            Console.Clear();

            Console.WriteLine("Enter Book Subject :");
            string subject = Console.ReadLine();
            Console.WriteLine("Enter Book Edition :");
            int edition = int.Parse(Console.ReadLine());

            return new NonFictionBook(title, author, category, isbn, subject, edition);
        }

        private static void RemoveBook(Library library)
        {
            Console.Clear();

            Console.WriteLine("Enter Book ISBN To Remove It:");
            string isbn = Console.ReadLine();

            Book book = library.Books.Find(b => b.ISBN == isbn);

            if (book == null)
            {
                Console.WriteLine("Book not found.");
                return;
            }
            else
            {
                library.RemoveBook(book);
                Console.WriteLine("Book removed successfully.");
            }
        }

        private static void UpdateBook(Library library)
        {
            Console.Clear();

            Console.WriteLine("Enter Book ISBN To Update It:");
            string isbn = Console.ReadLine();

            Book book = library.Books.Find(b => b.ISBN == isbn);

            if (book == null)
            {
                Console.WriteLine("Book not found.");
                return;
            }
            else
            {
                UpdateCommonBookDetails(book);

                switch (book)
                {
                    case FictionBook fictionBook:
                        UpdateFictionBookDetails(fictionBook);
                        break;
                    case NonFictionBook nonFictionBook:
                        UpdateNonFictionBookDetails(nonFictionBook);
                        break;
                }

                Console.WriteLine("Book updated successfully.");
            }
        }

        private static void UpdateCommonBookDetails(Book book)
        {
            Console.Clear();

            Console.WriteLine("Enter New Title :");
            book.Title = Console.ReadLine();
            Console.WriteLine("Enter New Author :");
            book.Author = Console.ReadLine();
            Console.WriteLine("Enter New Category :");
            book.Category = Console.ReadLine();
        }

        private static void UpdateFictionBookDetails(FictionBook fictionBook)
        {
            Console.Clear();

            Console.WriteLine("Enter New Series :");
            fictionBook.Series = Console.ReadLine();
            Console.WriteLine("Enter New Volume :");
            fictionBook.Volume = int.Parse(Console.ReadLine());
        }

        private static void UpdateNonFictionBookDetails(NonFictionBook nonFictionBook)
        {
            Console.Clear();

            Console.WriteLine("Enter New Subject :");
            nonFictionBook.Subject = Console.ReadLine();
            Console.WriteLine("Enter New Edition :");
            nonFictionBook.Edition = int.Parse(Console.ReadLine());
        }

        private static void ListBooks(Library library)
        {
            Console.Clear();

            Console.WriteLine("********** All Books **********");

            foreach (Book book in library.Books)
            {
                string details = $"{book.Title} by {book.Author} (ISBN: {book.ISBN}, Available: {book.IsAvailable})";

                if (book is FictionBook fictionBook)
                {
                    details += $", Series: {fictionBook.Series}, Volume: {fictionBook.Volume}";
                }
                else if (book is NonFictionBook nonFictionBook)
                {
                    details += $", Subject: {nonFictionBook.Subject}, Edition: {nonFictionBook.Edition}";
                }

                Console.WriteLine(details);
            }
        }

        private static void ManageUsers(Library library)
        {
            Console.Clear();

            while (true)
            {
                Console.WriteLine("""
                    ********** Manage Users **********
                    1- Add User
                    2- Remove User
                    3- update User
                    4- List Users
                    5- Back
                    """);

                Console.Write("Enter your choice: ");

                string userChoice = Console.ReadLine();

                switch (userChoice)
                {
                    case "1":
                        AddUser(library);
                        break;
                    case "2":
                        RemoveUser(library);
                        break;
                    case "3":
                        UpdateUser(library);
                        break;
                    case "4":
                        ListUsers(library);
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
            }   }
        }

        private static void AddUser(Library library)
        {
            Console.Clear();

            Console.WriteLine("Enter User Type [1 For Member , 2 For Librarian]");

            string userType = Console.ReadLine();

            Console.WriteLine("Enter User Name :");
            string name = Console.ReadLine();

            Console.WriteLine("Enter User ID :");
            string id = Console.ReadLine();

            User user = userType == "1" ? AddMember(id, name) : AddLibrarian(id, name);

            library.AddUser(user);

            Console.WriteLine("User added successfully.");
        }

        private static Member AddMember(string id, string name)
        {
            Console.Clear();

            Console.WriteLine("Enter User Membership Type :");

            MembershipType membershipType = Enum.Parse<MembershipType>(Console.ReadLine(), true);

            return new Member(id, name, membershipType);
        }

        private static Librarian AddLibrarian(string id, string name)
        {
            Console.Clear();

            Console.WriteLine("Enter User EmployeeId :");

            string employeeId = Console.ReadLine();

            return new Librarian(id, name, employeeId);
        }

        private static void RemoveUser(Library library)
        {
            Console.Clear();

            Console.WriteLine("Enter User ID To Remove It:");
            string id = Console.ReadLine();

            User user = library.Users.Find(u => u.Id == id);

            if (user == null)
            {
                Console.WriteLine("User not found.");
                return;
            }
            else
            {
                library.RemoveUser(user);
                Console.WriteLine("User removed successfully.");
            }
        }

        private static void UpdateUser(Library library)
        {
            Console.Clear();

            Console.WriteLine("Enter User ID To Update It:");
            string id = Console.ReadLine();

            User user = library.Users.Find(u => u.Id == id);

            if (user == null)
            {
                Console.WriteLine("User not found.");
                return;
            }
            else
            {
                Console.WriteLine("Enter New Name :");
                user.Name = Console.ReadLine();

                if (user is Member member)
                {
                    Console.WriteLine("Enter New Membership Type :");
                    member.MembershipType = Enum.Parse<MembershipType>(Console.ReadLine(), true);
                }
                else if (user is Librarian librarian)
                {
                    Console.WriteLine("Enter New Employee ID :");
                    librarian.EmployeeId = Console.ReadLine();
                }
                Console.WriteLine("User updated successfully.");
            }
        }

        private static void ListUsers(Library library)
        {
            Console.Clear();

            Console.WriteLine("********** All Users **********");
            foreach (User user in library.Users)
            {
                string details = $"{user.Name} (ID: {user.Id})";

                if (user is Member member)
                {
                    details += $", Membership Type: {member.MembershipType}";
                }
                else if (user is Librarian librarian)
                {
                    details += $", Employee ID: {librarian.EmployeeId}";
                }
                Console.WriteLine(details);
            }
        }

        private static void BorrowReturnBooks(Library library)
        {
            Console.Clear();

            while (true)
            {
                Console.WriteLine("""
                    ********** Borrow/Return Books **********
                    1- Borrow Book
                    2- Return Book
                    3- List Borrowed Books
                    4- Back
                    """);

                Console.Write("Enter your choice: ");
                string userChoice = Console.ReadLine();

               switch (userChoice)
                {
                    case "1":
                        BorrowBook(library);
                        break;
                    case "2":
                        ReturnBook(library);
                        break;
                    case "3":
                        ListBorrowedBooks(library);
                        break;
                    case "4":
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }

        }

        private static void BorrowBook(Library library)
        {
            Console.Clear();

            Console.WriteLine("Enter User ID :");
            string userId = Console.ReadLine();

            User user = library.Users.Find(u => u.Id == userId);

            if (user == null)
            {
                Console.WriteLine("User not found.");
                return;
            }

            Console.WriteLine("Enter Book ISBN To Borrow :");
            string isbn = Console.ReadLine();
            Book book = library.Books.Find(b => b.ISBN == isbn);

            if (book == null)
            {
                Console.WriteLine("Book not found.");
                return;
            }
            if (!book.IsAvailable)
            {
                Console.WriteLine("Book is not available.");
                return;
            }

            user.BorrowBook(book);
            Console.WriteLine("Book borrowed successfully.");
        }

        private static void ReturnBook(Library library)
        {
            Console.Clear();

            Console.WriteLine("Enter User ID :");
            string userId = Console.ReadLine();
            User user = library.Users.Find(u => u.Id == userId);

            if (user == null)
            {
                Console.WriteLine("User not found.");
                return;
            }

            Console.WriteLine("Enter Book ISBN To Return :");
            string isbn = Console.ReadLine();
            Book book = library.Books.Find(b => b.ISBN == isbn);

            if (book == null)
            {
                Console.WriteLine("Book not found.");
                return;
            }
            user.ReturnBook(book);
            Console.WriteLine("Book returned successfully.");
        }

        private static void ListBorrowedBooks(Library library)
        {
            Console.Clear();

            Console.WriteLine("********** Borrowed Books **********");
            foreach (var user in library.Users)
            {
                foreach (var book in user.BorrowedBooks)
                {
                    Console.WriteLine($"{user.Name} borrowed {book.Title}");
                }
            }
        }

        private static void Search(Library library)
        {
            Console.Clear();

            while (true)
            {
                Console.WriteLine("""
                    ********** Search **********
                    1- Search Books
                    2- Search Users
                    3- Back
                    """);

                Console.Write("Enter your choice: ");
                string userChoice = Console.ReadLine();

                switch (userChoice)
                {
                    case "1":
                        SearchBooks(library);
                        break;
                    case "2":
                        SearchUsers(library);
                        break;
                    case "3":
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        private static void SearchBooks(Library library)
        {
            Console.Clear();

            Console.WriteLine("Enter Search Criteria :");
            string criteria = Console.ReadLine();

            List<Book> searchResults = library.SearchBooks(criteria);

            Console.WriteLine("********** Search Results **********");

            foreach (Book book in searchResults)
            {
                if (book is FictionBook fictionBook)
                {
                    Console.WriteLine($"{book.Title} by {book.Author} (ISBN: {book.ISBN}, Series: {fictionBook.Series}, Volume: {fictionBook.Volume})");
                }
                else if (book is NonFictionBook nonFictionBook)
                {
                    Console.WriteLine($"{book.Title} by {book.Author} (ISBN: {book.ISBN}, Subject: {nonFictionBook.Subject}, Edition: {nonFictionBook.Edition})");
                }
                else
                {
                    Console.WriteLine($"{book.Title} by {book.Author} (ISBN: {book.ISBN})");
                }
            }
        }

        private static void SearchUsers(Library library)
        {
            Console.Clear();

            Console.WriteLine("Enter Search Criteria :");
            string criteria = Console.ReadLine();

            List<User> searchResults = library.SearchUsers(criteria);

            Console.WriteLine("********** Search Results **********");

            foreach (User user in searchResults)
            {
                if (user is Member member)
                {
                    Console.WriteLine($"{user.Name} (ID: {user.Id}, Membership Type: {member.MembershipType})");
                }
                else if (user is Librarian librarian)
                {
                    Console.WriteLine($"{user.Name} (ID: {user.Id}, Employee ID: {librarian.EmployeeId})");
                }
                else
                {
                    Console.WriteLine($"{user.Name} (ID: {user.Id})");
                }
            }
        }
    }
}
