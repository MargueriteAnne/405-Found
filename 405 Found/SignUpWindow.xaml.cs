using _405Found;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace _405_Found
{
    /// <summary>
    /// Interaction logic for SignUpWindow.xaml
    /// </summary>
    public partial class SignUpWindow : Window
    {
        private cs_405Found? dbContext;


        public SignUpWindow()
        {
            InitializeComponent();
            InitializeDbContext();
        }

        private void InitializeDbContext()
        {
            string connectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=405_Found;Integrated Security=True;Encrypt=True";

            var optionsBuilder = new DbContextOptionsBuilder<cs_405Found>();
            optionsBuilder.UseSqlServer(connectionString);

            dbContext = new cs_405Found(optionsBuilder.Options);
        }

        private void signUp_Click(object sender, RoutedEventArgs e)
        {
            // Get the user data from the TextBoxes
            string usernameText = txtUsername.Text.Trim();
            string passwordText = txtPassword.Password.Trim();
            string emailText = txtEmail.Text.Trim();

            // Check if username and password are not empty
            if (!string.IsNullOrEmpty(usernameText) && !string.IsNullOrEmpty(passwordText) && !string.IsNullOrEmpty(emailText))
            {
                // Check if the user with the same username already exists
                var existingUser = dbContext.Users.FirstOrDefault(u => u.Name.ToLower() == usernameText.ToLower());
                if (existingUser != null)
                {
                    MessageBox.Show("Username already exists. Please choose a different one.", "Sign Up Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Create a new user object
                var newUser = new User
                {
                    Name = usernameText,
                    SavedPassword = passwordText,
                    Email = emailText
                };

                // Add the new user to the database
                dbContext.Users.Add(newUser);
                dbContext.SaveChanges();

                MessageBox.Show("Sign Up Successful. You can now log in with your credentials.", "Sign Up Success", MessageBoxButton.OK, MessageBoxImage.Information);

                // Close the sign-up window
                // Show the main window
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();

                
            }
            else
            {
                MessageBox.Show("Please fill in all fields.", "Sign Up Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


    }
}

