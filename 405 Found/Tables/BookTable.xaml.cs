using _405Found;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using static _405_Found.MainWindow;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace _405_Found.Tables
{
    public partial class BookTable : Window
    {
        private readonly cs_405Found dbContext;

        public BookTable()
        {
            InitializeComponent();
            string connectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=405_Found;Integrated Security=True;Encrypt=True";

            var optionsBuilder = new DbContextOptionsBuilder<cs_405Found>();
            optionsBuilder.UseSqlServer(connectionString);
            
            dbContext = new cs_405Found(optionsBuilder.Options);

            string username = UserSession.LoggedInUsername;

            

            ShowAllBooks();
        }

        //shows the book list of the user, using User_Id as reference
        private void ShowAllBooks()
        {
            try
            {
                
                // Get the current user ID
                int currentUserId = GetCurrentUserId(UserSession.LoggedInUsername);
                // Filter books by current user ID
                var userBooks = dbContext.Books.Where(b => b.User_Id == currentUserId).ToList();

                // Assuming BooksListBox is the ListBox in your XAML
                BooksListBox.ItemsSource = userBooks;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading Books: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //adds new books to the list
        private void AddBook_Click(object sender, RoutedEventArgs e)
        {
            int currentUserId = GetCurrentUserId(UserSession.LoggedInUsername);
            string bookName = bookNameTextBox.Text;
            string genre = (genreComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
            string status = (statusComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();


            
           

            if (currentUserId == -1)
            {
                MessageBox.Show("Error: User not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!string.IsNullOrEmpty(bookName) && !string.IsNullOrEmpty(genre) && !string.IsNullOrEmpty(status))
            {
                try
                {
                    // Define the INSERT INTO query
                    string query = $"INSERT INTO Books (BookName, Genre, Status, User_Id) " +
                                   $"VALUES ('{bookName}', '{genre}', '{status}', {currentUserId})";

                    // Define the connection string
                    string connectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=405_Found;Integrated Security=True;Encrypt=True";

                    // Create a SqlConnection using the connection string
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        // Open the connection
                        connection.Open();

                        // Create a SqlCommand with the query and connection
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            // Execute the command
                            int rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Book added successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                                // Clear input fields after adding the book
                                bookNameTextBox.Text = "";
                                genreComboBox.SelectedIndex = -1;
                                statusComboBox.SelectedIndex = -1;

                                // Refresh the list of books
                                ShowAllBooks();
                            }
                            else
                            {
                                MessageBox.Show("Failed to add book.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error adding book: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Please fill in all fields.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private int GetCurrentUserId(string username)
        {
            var user = dbContext.Users.FirstOrDefault(u => u.Name.ToLower() == username.ToLower());
            return user != null ? user.User_Id : -1; // Return -1 if user not found, or return the user's Id
        }

       
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (BooksListBox.SelectedItem != null)
            {
                try
                {
                    // Get the selected book
                    var selectedBook = (Books)BooksListBox.SelectedItem;

                    // Remove the book from the database and save changes
                    dbContext.Books.Remove(selectedBook);
                    dbContext.SaveChanges();

                    // Refresh the list of books
                    ShowAllBooks();

                    MessageBox.Show("Book deleted successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting book: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a book to delete.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
