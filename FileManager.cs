﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using LibrarySystem.Models;

namespace LibrarySystem
{
    public class FileManager
    {
        public static void SaveData(string filePath, List<Book> books, List<User> users)
        {
            var data = new { Books = books, Users = users };

            string jsonData = JsonSerializer.Serialize(data);

            File.WriteAllText(filePath, jsonData);
        }

        public static void LoadData(string filePath, out List<Book> books, out List<User> users)
        {
            string jsonData = File.ReadAllText(filePath);

            var data = JsonSerializer.Deserialize<Library>(jsonData);

            books = data.Books;
            users = data.Users;
        }
    }
}
