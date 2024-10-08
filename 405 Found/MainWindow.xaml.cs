using _405Found;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using _405_Found.Tables;
using Microsoft.VisualBasic.ApplicationServices;

namespace _405_Found
{


    public partial class MainWindow : Window
    {

        
        private cs_405Found dbContext;

        public MainWindow()
        {
            InitializeComponent();
            string connectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=405_Found;Integrated Security=True;Encrypt=True";

            var optionsBuilder = new DbContextOptionsBuilder<cs_405Found>();
            optionsBuilder.UseSqlServer(connectionString);

            dbContext = new cs_405Found(optionsBuilder.Options);

            

        }

        public static class UserSession
        {
            public static string? LoggedInUsername { get; set; }
        }

        private void Log_in_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=405_Found;Integrated Security=True;Encrypt=True";

            var optionsBuilder = new DbContextOptionsBuilder<cs_405Found>();
            optionsBuilder.UseSqlServer(connectionString);
            {
                // Get the username and password from the TextBoxes
                string usernameText = username.Text.Trim();
                string passwordText = password.Password;

                UserSession.LoggedInUsername = usernameText;

                var user = dbContext.Users.FirstOrDefault(u => u.Name.ToLower() == usernameText.ToLower());



                if (user != null)
                {
                    // Check if the provided password matches the stored password
                    if (user.SavedPassword == passwordText)
                    {
                        // Login successful, navigate to the files window
                        BookTable bookTable = new BookTable();
                        bookTable.Show();
                        this.Close();
                    }
                    else
                    {
                        // Login failed, show error message
                        MessageBox.Show("Invalid password. Please try again.", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    // User not found, show error message
                    MessageBox.Show("User not found. Please check your username.", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

      

        private void Signup_Click(object sender, RoutedEventArgs e)
        {
            // Open the sign-up window
            SignUpWindow signUpWindow = new SignUpWindow();
            signUpWindow.Show();
            this.Close();
        }
    }
}
