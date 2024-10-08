using _405Found;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace _405_Found
{

    public partial class SetData : Window
    {
        /*public class File
        {
            public int FileId { get; set; }
            public string? FileName { get; set; } // Nullable string

         
        }
        */

        private cs_405Found cs;

        public SetData()
        {
            InitializeComponent();
            cs = new cs_405Found();
            InitializeDB();
        }

        private void InitializeDB()
        {
            cs.Database.EnsureCreated();
            if (!cs.Users.Any())
            {
                SeedDB();
            }
            ShowData();
        }

        private void SeedDB()
        {
            var user1 = new User { Name = "Ana Santos", SavedPassword = "password123", Email = "ana@hotmail.com" };
            var user2 = new User { Name = "Carmen Fonseca", SavedPassword = "password456", Email = "carmen@hotmail.com"};
            var user3 = new User { Name = "Marcos Barbosa", SavedPassword = "password789", Email = "marcos@hotmail.com" };

            var Book1 = new Books { BookName = "Dipu Number 2", Genre = "Adventure", Status = "Read",  User = user1 };
            var Book2 = new Books { BookName = "Master Class Salauddin", Genre = "Sport", Status = "Read", User = user1 };
            var Book3 = new Books { BookName = "Venenos de Deus, remédios do Diabo", Genre = "Romance", Status = "Read", User = user2 };
            var Book4 = new Books { BookName = "Lalsalu", Genre = "Romance", Status = "Want to read", User = user3 };

            cs.Users.AddRange(new User[] { user1, user2, user3 });
      
            cs.Books.AddRange(new Books[] { Book1, Book2, Book3, Book4});

            cs.SaveChanges();
        }

        private void ShowData()
        {
            var usersWithBooks = cs.Users.Include(u => u.Books).ToList();

            foreach (var user in usersWithBooks)
            {
                Console.WriteLine($"User: {user.Name}");
                foreach (var Books in user.Books)
                {
                    Console.WriteLine($" - Books: {Books.BookName}, Genre: {Books.Genre}, Status: {Books.Status}");
                }
                Console.WriteLine();
            }
        }
    }
}
